using MediatR;

namespace RisedorApi.Application.Commands.Order;

public record DeleteOrderCommand(int Id) : IRequest<bool>;
