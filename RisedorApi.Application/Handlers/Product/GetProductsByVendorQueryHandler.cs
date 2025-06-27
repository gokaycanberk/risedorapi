using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Queries.Product;
using RisedorApi.Domain.Entities;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Product;

public class GetProductsByVendorQueryHandler
    : IRequestHandler<GetProductsByVendorQuery, List<Domain.Entities.Product>>
{
    private readonly ApplicationDbContext _context;

    public GetProductsByVendorQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.Entities.Product>> Handle(
        GetProductsByVendorQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _context.Products
            .Include(p => p.Vendor)
            .Where(p => p.VendorId == request.VendorId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
