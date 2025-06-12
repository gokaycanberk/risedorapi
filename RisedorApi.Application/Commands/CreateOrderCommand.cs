using MediatR;

namespace RisedorApi.Application.Commands;

public record CreateOrderCommand(int SupermarketId, int VendorId, List<OrderItemDto> Items)
    : IRequest<int>;

public record OrderItemDto(int ProductId, int Quantity);
