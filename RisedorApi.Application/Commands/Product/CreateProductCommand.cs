using MediatR;

namespace RisedorApi.Application.Commands.Product;

public record CreateProductCommand(int VendorId, string Name, decimal Price, int StockQuantity)
    : IRequest<int>;
