using RisedorApi.Domain.Enums;

namespace RisedorApi.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int SupermarketId { get; set; }
    public int VendorId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }

    // Navigation properties
    public User Supermarket { get; set; } = null!;
    public User Vendor { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Order()
    {
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public Order(int supermarketId, int vendorId)
        : this()
    {
        SupermarketId = supermarketId;
        VendorId = vendorId;
    }
}
