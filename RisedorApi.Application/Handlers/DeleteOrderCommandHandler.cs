using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands;
using RisedorApi.Infrastructure.Persistence;

namespace RisedorApi.Application.Handlers;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteOrderCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order == null)
            return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
