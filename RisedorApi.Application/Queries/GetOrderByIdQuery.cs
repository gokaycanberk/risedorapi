using MediatR;
using RisedorApi.Domain.Entities;

namespace RisedorApi.Application.Queries;

public record GetOrderByIdQuery(int Id) : IRequest<Order>;
