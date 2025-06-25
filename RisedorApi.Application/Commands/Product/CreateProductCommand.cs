using MediatR;

namespace RisedorApi.Application.Commands.Product;

public record CreateProductCommand(
    string Name,
    string UpcCode,
    string ItemCode,
    string Description,
    string Size,
    int CasePack,
    decimal CasePrice,
    decimal UnitPrice,
    string? ImageUrl,
    int Stock,
    int VendorId
) : IRequest<int>;
