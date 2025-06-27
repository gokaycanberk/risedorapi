using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int SalesRepUserId { get; set; }
    public int SupermarketUserId { get; set; }
    public string BuyerInfo { get; set; } = string.Empty;
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public int Id { get; set; }
    public string ProductItemCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int VendorId { get; set; }
    public decimal CasePrice { get; set; }
}
