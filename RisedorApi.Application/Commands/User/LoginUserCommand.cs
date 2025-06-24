using MediatR;

namespace RisedorApi.Application.Commands.User;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginUserResponse>;

public record LoginUserResponse(string Token, string Email, string Username, string Role);
