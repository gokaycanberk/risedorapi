using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Queries;
using RisedorApi.Domain.Entities;
using RisedorApi.Infrastructure.Persistence;

namespace RisedorApi.Application.Handlers;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order?>
{
    private readonly ApplicationDbContext _context;

    public GetOrderByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.Supermarket)
            .Include(o => o.Vendor)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
    }
}
