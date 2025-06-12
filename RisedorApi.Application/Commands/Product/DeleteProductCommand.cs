using MediatR;

namespace RisedorApi.Application.Commands.Product;

public record DeleteProductCommand(int Id) : IRequest<bool>;
