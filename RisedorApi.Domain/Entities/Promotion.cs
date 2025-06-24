namespace RisedorApi.Domain.Entities;

public class Promotion
{
    public int Id { get; set; }
    public int VendorId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public User Vendor { get; set; } = null!;
}
