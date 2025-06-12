using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.Product;

public record GetProductByIdQuery(int Id) : IRequest<Domain.Entities.Product?>;
