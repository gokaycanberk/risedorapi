using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Queries.User;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.User;

public class GetUserByEmailQueryHandler
    : IRequestHandler<GetUserByEmailQuery, Domain.Entities.User?>
{
    private readonly ApplicationDbContext _context;

    public GetUserByEmailQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.User?> Handle(
        GetUserByEmailQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Users.FirstOrDefaultAsync(
            u => u.Email == request.Email,
            cancellationToken
        );
    }
}
