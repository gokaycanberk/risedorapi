using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries;

public record GetOrdersQuery : IRequest<IEnumerable<Order>>;
