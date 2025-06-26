using MediatR;
using RisedorApi.Application.DTOs;

namespace RisedorApi.Application.Queries;

public record GetOrderByIdQuery(int Id) : IRequest<OrderDto?>;
