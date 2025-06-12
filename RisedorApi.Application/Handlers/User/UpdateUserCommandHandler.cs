using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.User;
using RisedorApi.Infrastructure.Persistence;

namespace RisedorApi.Application.Handlers.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public UpdateUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user == null)
            return false;

        // Update only allowed fields
        user.Name = request.Name;
        user.Email = request.Email;
        user.Role = request.Role;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
