using MediatR;

namespace RisedorApi.Application.Commands;

public record DeleteOrderCommand(int Id) : IRequest<bool>;
