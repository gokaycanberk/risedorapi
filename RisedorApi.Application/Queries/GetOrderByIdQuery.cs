using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries;

public record GetOrderByIdQuery(int OrderId) : IRequest<Order?>;
