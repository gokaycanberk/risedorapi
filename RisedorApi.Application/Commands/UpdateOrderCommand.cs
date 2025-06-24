using MediatR;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Commands;

public record UpdateOrderCommand(int Id, OrderStatus Status) : IRequest<bool>;
