using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Queries;
using RisedorApi.Domain.Entities;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly ApplicationDbContext _context;

    public GetOrderByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);
    }
}
