namespace RisedorApi.Domain.Entities;

public class Promotion
{
    public int Id { get; private set; }
    public int VendorId { get; private set; }
    public string Description { get; private set; }
    public decimal DiscountPercentage { get; private set; }
    public DateTime ValidFrom { get; private set; }
    public DateTime ValidTo { get; private set; }

    protected Promotion() { } // For EF Core

    public Promotion(
        int vendorId,
        string description,
        decimal discountPercentage,
        DateTime validFrom,
        DateTime validTo
    )
    {
        VendorId = vendorId;
        Description = description;
        DiscountPercentage = discountPercentage;
        ValidFrom = validFrom;
        ValidTo = validTo;
    }

    // Navigation property
    public User Vendor { get; private set; }
}
