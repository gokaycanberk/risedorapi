using MediatR;

namespace RisedorApi.Application.Commands.Product;

public record UpdateProductCommand(int Id, string Name, decimal Price, int StockQuantity)
    : IRequest<bool>;
