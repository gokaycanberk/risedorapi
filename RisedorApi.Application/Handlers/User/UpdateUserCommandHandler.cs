using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.User;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<UpdateUserCommand> _validator;

    public UpdateUserCommandHandler(
        ApplicationDbContext context,
        IValidator<UpdateUserCommand> validator
    )
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user == null)
            return false;

        // Check if email is already taken by another user
        var existingUser = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == request.Email && u.Id != request.Id,
            cancellationToken
        );
        if (existingUser != null)
        {
            throw new ValidationException("Email is already taken");
        }

        // Check if username is already taken by another user
        existingUser = await _context.Users.FirstOrDefaultAsync(
            u => u.Username == request.Username && u.Id != request.Id,
            cancellationToken
        );
        if (existingUser != null)
        {
            throw new ValidationException("Username is already taken");
        }

        // Update only allowed fields
        user.Username = request.Username;
        user.Email = request.Email;
        user.Role = request.Role;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
