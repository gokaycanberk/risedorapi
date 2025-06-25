using MediatR;

namespace RisedorApi.Application.Commands.Product;

public record UpdateProductCommand(
    int Id,
    string Name,
    string UpcCode,
    string ItemCode,
    string Description,
    string Size,
    int CasePack,
    decimal CasePrice,
    decimal UnitPrice,
    string? ImageUrl,
    int Stock
) : IRequest<bool>;
