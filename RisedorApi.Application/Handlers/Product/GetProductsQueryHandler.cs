using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.DTOs;
using RisedorApi.Application.Queries.Product;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Product;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly ApplicationDbContext _context;

    public GetProductsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        var products = await _context.Products
            .Include(p => p.Vendor)
            .ToListAsync(cancellationToken);

        return products.Select(ProductDto.FromProduct);
    }
}
