using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.Product;

public record GetProductsQuery : IRequest<IEnumerable<Domain.Entities.Product>>;
