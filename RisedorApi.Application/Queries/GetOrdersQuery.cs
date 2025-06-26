using MediatR;
using RisedorApi.Application.DTOs;

namespace RisedorApi.Application.Queries;

public record GetOrdersQuery : IRequest<List<OrderDto>>;
