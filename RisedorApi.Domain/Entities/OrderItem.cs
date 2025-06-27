namespace RisedorApi.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public string ProductItemCode { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int VendorId { get; set; }
    public decimal CasePrice { get; set; }
    public int OrderId { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!;
    public User Vendor { get; set; } = null!;

    protected OrderItem() { }

    public OrderItem(string productItemCode, int quantity, int vendorId, decimal casePrice)
    {
        ProductItemCode = productItemCode;
        Quantity = quantity;
        VendorId = vendorId;
        CasePrice = casePrice;
    }
}
