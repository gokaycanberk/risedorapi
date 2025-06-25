using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.Product;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Product;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly ApplicationDbContext _context;

    public DeleteProductHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        DeleteProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = await _context.Products.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );

        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
