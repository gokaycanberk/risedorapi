using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.Product;
using RisedorApi.Infrastructure.Data;

namespace RisedorApi.Application.Handlers.Product;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<UpdateProductCommand> _validator;

    public UpdateProductHandler(
        ApplicationDbContext context,
        IValidator<UpdateProductCommand> validator
    )
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        // Validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var product = await _context.Products.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );

        if (product == null)
            return false;

        // Check if UPC Code is unique
        var existingProduct = await _context.Products.FirstOrDefaultAsync(
            p => p.UpcCode == request.UpcCode && p.Id != request.Id,
            cancellationToken
        );
        if (existingProduct != null)
        {
            throw new ValidationException("UPC Code must be unique");
        }

        // Check if Item Code is unique
        existingProduct = await _context.Products.FirstOrDefaultAsync(
            p => p.ItemCode == request.ItemCode && p.Id != request.Id,
            cancellationToken
        );
        if (existingProduct != null)
        {
            throw new ValidationException("Item Code must be unique");
        }

        // Update fields
        product.Name = request.Name;
        product.UpcCode = request.UpcCode;
        product.ItemCode = request.ItemCode;
        product.Description = request.Description;
        product.Size = request.Size;
        product.CasePack = request.CasePack;
        product.CasePrice = request.CasePrice;
        product.UnitPrice = request.UnitPrice;
        product.ImageUrl = request.ImageUrl;
        product.Stock = request.Stock;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
