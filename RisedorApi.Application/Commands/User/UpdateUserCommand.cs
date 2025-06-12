using MediatR;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Commands.User;

public record UpdateUserCommand(int Id, string Name, string Email, UserRole Role) : IRequest<bool>;
