using MediatR;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Commands.User;

public record CreateUserCommand(string Username, string Email, string Password, UserRole Role)
    : IRequest<Domain.Entities.User>;
