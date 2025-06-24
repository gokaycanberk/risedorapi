using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.User;
using RisedorApi.Infrastructure.Data;
using RisedorApi.Infrastructure.Services;
using System.Security.Cryptography;
using System.Text;

namespace RisedorApi.Application.Handlers.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<LoginUserCommand> _validator;
    private readonly IJwtAuthManager _jwtAuthManager;

    public LoginUserCommandHandler(
        ApplicationDbContext context,
        IValidator<LoginUserCommand> validator,
        IJwtAuthManager jwtAuthManager
    )
    {
        _context = context;
        _validator = validator;
        _jwtAuthManager = jwtAuthManager;
    }

    public async Task<LoginUserResponse> Handle(
        LoginUserCommand request,
        CancellationToken cancellationToken
    )
    {
        // Validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // Find user by email
        var user = await _context.Users.FirstOrDefaultAsync(
            u => u.Email == request.Email,
            cancellationToken
        );

        if (user == null)
        {
            throw new ValidationException("Invalid email or password");
        }

        // Verify password
        var hashedPassword = HashPassword(request.Password);
        if (user.PasswordHash != hashedPassword)
        {
            throw new ValidationException("Invalid email or password");
        }

        // Generate JWT token
        var token = _jwtAuthManager.GenerateToken(user);

        return new LoginUserResponse(
            Token: token,
            Email: user.Email,
            Username: user.Username,
            Role: user.Role.ToString()
        );
    }

    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
