using MediatR;
using RisedorApi.Application.DTOs;

namespace RisedorApi.Application.Queries.Product;

public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;
