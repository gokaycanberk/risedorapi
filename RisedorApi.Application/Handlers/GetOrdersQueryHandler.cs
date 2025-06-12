using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Queries;
using RisedorApi.Domain.Entities;
using RisedorApi.Infrastructure.Persistence;

namespace RisedorApi.Application.Handlers;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<Order>>
{
    private readonly ApplicationDbContext _context;

    public GetOrdersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.Supermarket)
            .Include(o => o.Vendor)
            .ToListAsync(cancellationToken);
    }
}
