using MediatR;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Commands.Order;

public record UpdateOrderStatusCommand(int Id, OrderStatus Status) : IRequest<bool>;
