using MediatR;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Commands.User;

public record CreateUserCommand(string Name, string Email, string Password, UserRole Role)
    : IRequest<int>;
