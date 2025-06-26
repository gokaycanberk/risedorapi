using MediatR;

namespace RisedorApi.Application.Commands.Order;

public class CreateOrderCommand : IRequest<int>
{
    public int SalesRepUserId { get; set; }
    public int SupermarketUserId { get; set; }
    public string BuyerInfo { get; set; } = string.Empty;
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class CreateOrderItemDto
{
    public string ProductItemCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int VendorId { get; set; }
}
