using MediatR;
using RisedorApi.Application.DTOs;

namespace RisedorApi.Application.Queries.Order;

public record GetOrdersBySupermarketQuery(int SupermarketId) : IRequest<List<OrderDto>>;
