using MediatR;
using RisedorApi.Application.DTOs;

namespace RisedorApi.Application.Queries.Product;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto?>;
