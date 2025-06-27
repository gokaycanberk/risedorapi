using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.DTOs;
using RisedorApi.Application.Queries.Order;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Order;

public class GetOrdersBySalesRepQueryHandler
    : IRequestHandler<GetOrdersBySalesRepQuery, List<OrderDto>>
{
    private readonly ApplicationDbContext _context;

    public GetOrdersBySalesRepQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDto>> Handle(
        GetOrdersBySalesRepQuery request,
        CancellationToken cancellationToken
    )
    {
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.SalesRepUserId == request.SalesRepId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return orders
            .Select(
                o =>
                    new OrderDto
                    {
                        Id = o.Id,
                        SalesRepUserId = o.SalesRepUserId,
                        SupermarketUserId = o.SupermarketUserId,
                        BuyerInfo = o.BuyerInfo,
                        Status = o.Status,
                        CreatedAt = o.CreatedAt,
                        TotalAmount = o.TotalAmount,
                        Items = o.Items
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
                    }
            )
            .ToList();
    }
}
