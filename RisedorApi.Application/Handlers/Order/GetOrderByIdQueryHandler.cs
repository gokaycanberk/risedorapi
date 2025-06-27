using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.DTOs;
using RisedorApi.Application.Queries;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Order;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ApplicationDbContext _context;

    public GetOrderByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDto?> Handle(
        GetOrderByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (order == null)
            return null;

        return new OrderDto
        {
            Id = order.Id,
            SalesRepUserId = order.SalesRepUserId,
            SupermarketUserId = order.SupermarketUserId,
            BuyerInfo = order.BuyerInfo,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            TotalAmount = order.TotalAmount,
            Items = order.Items
                .Select(
                    i =>
                        new OrderItemDto
                        {
                            Id = i.Id,
                            ProductItemCode = i.ProductItemCode,
                            Quantity = i.Quantity,
                            VendorId = i.VendorId,
                            CasePrice = i.CasePrice
                        }
                )
                .ToList()
        };
    }
}
