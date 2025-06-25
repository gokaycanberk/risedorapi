using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.DTOs;
using RisedorApi.Application.Queries.Product;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Product;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly ApplicationDbContext _context;

    public GetProductByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var product = await _context.Products
            .Include(p => p.Vendor)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        return product == null ? null : ProductDto.FromProduct(product);
    }
}
