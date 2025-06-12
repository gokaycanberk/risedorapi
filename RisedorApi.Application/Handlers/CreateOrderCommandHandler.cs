using MediatR;
using RisedorApi.Application.Commands;
using RisedorApi.Domain.Entities;
using RisedorApi.Domain.Enums;
using RisedorApi.Infrastructure.Persistence;

namespace RisedorApi.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateOrderCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.SupermarketId, request.VendorId);

        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);

        foreach (var item in request.Items)
        {
            var orderItem = new OrderItem(order.Id, item.ProductId, item.Quantity);
            _context.OrderItems.Add(orderItem);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
