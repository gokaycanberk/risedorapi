using FluentValidation;
using MediatR;
using RisedorApi.Application.Commands.User;
using RisedorApi.Application.Validators;
using RisedorApi.Domain.Entities;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Domain.Entities.User>
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(
        ApplicationDbContext context,
        IValidator<CreateUserCommand> validator
    )
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Domain.Entities.User> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        // Validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = new Domain.Entities.User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password, // In a real app, hash this password
            Role = request.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}
