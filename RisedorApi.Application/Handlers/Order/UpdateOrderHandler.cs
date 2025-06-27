using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.Order;
using RisedorApi.Domain.Entities;
using RisedorApi.Infrastructure.Data;
using RisedorApi.Shared.Exceptions;

namespace RisedorApi.Application.Handlers.Order;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateOrderHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        // Start transaction
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // Get order with items
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null)
                return false;

            // Update basic info
            order.BuyerInfo = request.BuyerInfo;

            // Remove items that are not in the update request
            var itemsToRemove = order.Items
                .Where(
                    i =>
                        !request.Items.Any(
                            ri =>
                                ri.ProductItemCode == i.ProductItemCode && ri.VendorId == i.VendorId
                        )
                )
                .ToList();

            foreach (var item in itemsToRemove)
            {
                // Return stock for removed items
                var product = await _context.Products.FirstOrDefaultAsync(
                    p => p.ItemCode == item.ProductItemCode && p.VendorId == item.VendorId,
                    cancellationToken
                );
                if (product != null)
                {
                    product.Stock += item.Quantity;
                }
                order.Items.Remove(item);
            }

            // Update existing items and add new ones
            foreach (var itemDto in request.Items)
            {
                var existingItem = order.Items.FirstOrDefault(
                    i =>
                        i.ProductItemCode == itemDto.ProductItemCode
                        && i.VendorId == itemDto.VendorId
                );

                var product = await _context.Products.FirstOrDefaultAsync(
                    p => p.ItemCode == itemDto.ProductItemCode && p.VendorId == itemDto.VendorId,
                    cancellationToken
                );

                if (product == null)
                    throw new ApiException(
                        $"Product with ItemCode {itemDto.ProductItemCode} from vendor {itemDto.VendorId} not found"
                    );

                if (existingItem != null)
                {
                    // Update stock for quantity difference
                    var quantityDiff = itemDto.Quantity - existingItem.Quantity;
                    if (quantityDiff > 0 && product.Stock < quantityDiff)
                    {
                        throw new ApiException(
                            $"Insufficient stock for product {itemDto.ProductItemCode}. Available: {product.Stock}, Additional Requested: {quantityDiff}"
                        );
                    }
                    product.Stock -= quantityDiff;
                    existingItem.Quantity = itemDto.Quantity;
                    existingItem.CasePrice = product.CasePrice; // Update to current price
                }
                else
                {
                    // Check stock for new item
                    if (product.Stock < itemDto.Quantity)
                    {
                        throw new ApiException(
                            $"Insufficient stock for product {itemDto.ProductItemCode}. Available: {product.Stock}, Requested: {itemDto.Quantity}"
                        );
                    }

                    // Create new item
                    var newItem = new OrderItem(
                        itemDto.ProductItemCode,
                        itemDto.Quantity,
                        itemDto.VendorId,
                        product.CasePrice
                    )
                    {
                        OrderId = order.Id
                    };
                    order.Items.Add(newItem);
                    product.Stock -= itemDto.Quantity;
                }
            }

            // Recalculate total amount
            order.CalculateTotalAmount();

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
