using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries.Product;

public record GetProductsByVendorQuery(int VendorId) : IRequest<List<Domain.Entities.Product>>;
