namespace RisedorApi.Domain.Entities;

public class Product
{
    public int Id { get; private set; }
    public int VendorId { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }

    protected Product() { } // For EF Core

    public Product(int vendorId, string name, decimal price, int stockQuantity)
    {
        VendorId = vendorId;
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
    }

    // Navigation property
    public User Vendor { get; private set; }
}
