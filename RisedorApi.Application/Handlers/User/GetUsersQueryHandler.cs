using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Queries.User;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.User;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, IEnumerable<Domain.Entities.User>>
{
    private readonly ApplicationDbContext _context;

    public GetUsersQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.User>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }
}
