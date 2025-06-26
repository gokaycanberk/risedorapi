using MediatR;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Commands.Order;

public class UpdateOrderCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string BuyerInfo { get; set; } = string.Empty;
    public List<UpdateOrderItemDto> Items { get; set; } = new();
}

public class UpdateOrderItemDto
{
    public string ProductItemCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int VendorId { get; set; }
}
