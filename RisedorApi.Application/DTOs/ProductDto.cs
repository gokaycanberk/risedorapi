using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.DTOs;

public class ProductDto
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
    public string VendorName { get; set; } = null!;
    public string VendorEmail { get; set; } = null!;

    public static ProductDto FromProduct(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            UpcCode = product.UpcCode,
            ItemCode = product.ItemCode,
            Description = product.Description,
            Size = product.Size,
            CasePack = product.CasePack,
            CasePrice = product.CasePrice,
            UnitPrice = product.UnitPrice,
            ImageUrl = product.ImageUrl,
            Stock = product.Stock,
            VendorId = product.VendorId,
            VendorName = product.Vendor.Username,
            VendorEmail = product.Vendor.Email
        };
    }
}
