using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Queries.User;
using RisedorApi.Infrastructure.Persistence;

namespace RisedorApi.Application.Handlers.User;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Domain.Entities.User?>
{
    private readonly ApplicationDbContext _context;

    public GetUserByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.User?> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
    }
}
