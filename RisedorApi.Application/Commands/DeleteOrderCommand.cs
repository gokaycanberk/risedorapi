using MediatR;

namespace RisedorApi.Application.Commands;

public record DeleteOrderCommand(int OrderId) : IRequest<bool>;
