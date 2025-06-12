using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands;
using RisedorApi.Infrastructure.Persistence;

namespace RisedorApi.Application.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateOrderCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FindAsync(
            new object[] { request.OrderId },
            cancellationToken
        );

        if (order == null)
            return false;

        order.Status = request.Status;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
