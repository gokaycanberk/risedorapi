using RisedorApi.Domain.Enums;

namespace RisedorApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int SalesRepUserId { get; set; }
    public int SupermarketUserId { get; set; }
    public string BuyerInfo { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }

    // Navigation properties
    public User SalesRep { get; set; } = null!;
    public User Supermarket { get; set; } = null!;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public Order()
    {
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void CalculateTotalAmount()
    {
        TotalAmount = Items.Sum(item => item.CasePrice * item.Quantity);
    }
}
