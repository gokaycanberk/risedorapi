using MediatR;
using RisedorApi.Application.DTOs;

namespace RisedorApi.Application.Queries.Order;

public record GetOrdersBySalesRepQuery(int SalesRepId) : IRequest<List<OrderDto>>;
