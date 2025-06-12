using MediatR;

namespace RisedorApi.Application.Commands.User;

public record DeleteUserCommand(int Id) : IRequest<bool>;
