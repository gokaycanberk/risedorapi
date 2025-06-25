namespace RisedorApi.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string UpcCode { get; set; } = null!;
    public string ItemCode { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Size { get; set; } = null!;
    public int CasePack { get; set; }
    public decimal CasePrice { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ImageUrl { get; set; }
    public int Stock { get; set; }
    public int VendorId { get; set; }

    // Navigation properties
    public User Vendor { get; set; } = null!;
}
