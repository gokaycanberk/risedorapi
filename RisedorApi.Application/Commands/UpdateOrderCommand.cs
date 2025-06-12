using MediatR;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Commands;

public record UpdateOrderCommand(int OrderId, OrderStatus Status) : IRequest<bool>;
