using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.Order;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Order;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateOrderStatusHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UpdateOrderStatusCommand request,
        CancellationToken cancellationToken
    )
    {
        var order = await _context.Orders.FirstOrDefaultAsync(
            o => o.Id == request.Id,
            cancellationToken
        );

        if (order == null)
            return false;

        order.Status = request.Status;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
