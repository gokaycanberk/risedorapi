namespace RisedorApi.Domain.Entities;

public class OrderItem
{
    public int Id { get; private set; }
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }

    protected OrderItem() { } // For EF Core

    public OrderItem(int orderId, int productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    // Navigation properties
    public Order Order { get; private set; }
    public Product Product { get; private set; }
}
