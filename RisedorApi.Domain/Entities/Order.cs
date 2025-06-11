using RisedorApi.Domain.Enums;

namespace RisedorApi.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public int SupermarketId { get; private set; }
    public int VendorId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    protected Order() { } // For EF Core

    public Order(int supermarketId, int vendorId)
    {
        SupermarketId = supermarketId;
        VendorId = vendorId;
        OrderDate = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    // Navigation properties
    public User Supermarket { get; private set; }
    public User Vendor { get; private set; }
}
