using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.Order;
using RisedorApi.Domain.Entities;
using RisedorApi.Domain.Enums;
using RisedorApi.Infrastructure.Data;
using RisedorApi.Shared.Exceptions;

namespace RisedorApi.Application.Handlers.Order;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateOrderCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Validate users exist and have correct roles
        var salesRep = await _context.Users.FindAsync(
            new object[] { request.SalesRepUserId },
            cancellationToken
        );
        var supermarket = await _context.Users.FindAsync(
            new object[] { request.SupermarketUserId },
            cancellationToken
        );

        if (salesRep == null)
            throw new ApiException($"SalesRep with ID {request.SalesRepUserId} not found");
        if (supermarket == null)
            throw new ApiException($"Supermarket with ID {request.SupermarketUserId} not found");

        if (salesRep.Role != UserRole.SalesRep)
            throw new ApiException($"User {request.SalesRepUserId} is not a SalesRep");
        if (supermarket.Role != UserRole.Supermarket)
            throw new ApiException($"User {request.SupermarketUserId} is not a Supermarket");

        // Start transaction
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // Create order
            var order = new Domain.Entities.Order
            {
                SalesRepUserId = request.SalesRepUserId,
                SupermarketUserId = request.SupermarketUserId,
                BuyerInfo = request.BuyerInfo
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            // Create order items
            foreach (var itemDto in request.Items)
            {
                // Get product and validate
                var product = await _context.Products
                    .Include(p => p.Vendor)
                    .FirstOrDefaultAsync(
                        p =>
                            p.ItemCode == itemDto.ProductItemCode && p.VendorId == itemDto.VendorId,
                        cancellationToken
                    );

                if (product == null)
                    throw new ApiException(
                        $"Product with ItemCode {itemDto.ProductItemCode} from vendor {itemDto.VendorId} not found"
                    );

                if (product.Stock < itemDto.Quantity)
                    throw new ApiException(
                        $"Insufficient stock for product {itemDto.ProductItemCode}. Available: {product.Stock}, Requested: {itemDto.Quantity}"
                    );

                // Create order item with current product price
                var orderItem = new OrderItem(
                    itemDto.ProductItemCode,
                    itemDto.Quantity,
                    itemDto.VendorId,
                    product.UnitPrice
                )
                {
                    OrderId = order.Id
                };

                // Update product stock
                product.Stock -= itemDto.Quantity;
                _context.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync(cancellationToken);

            // Calculate total amount
            order.CalculateTotalAmount();
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return order.Id;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
