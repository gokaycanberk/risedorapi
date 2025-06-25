using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RisedorApi.Application.Commands.Product;
using RisedorApi.Infrastructure.Data;
using RisedorApi.Domain.Enums;

namespace RisedorApi.Application.Handlers.Product;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly ApplicationDbContext _context;
    private readonly IValidator<CreateProductCommand> _validator;

    public CreateProductHandler(
        ApplicationDbContext context,
        IValidator<CreateProductCommand> validator
    )
    {
        _context = context;
        _validator = validator;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate request
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Validation failed: {errors}");
        }

        // Verify vendor exists and has correct role
        var vendor = await _context.Users.FirstOrDefaultAsync(
            u => u.Id == request.VendorId,
            cancellationToken
        );
        if (vendor == null)
        {
            throw new ValidationException($"Vendor with ID {request.VendorId} does not exist");
        }
        if (vendor.Role != UserRole.Vendor && vendor.Role != UserRole.SalesRep)
        {
            throw new ValidationException(
                $"User with ID {request.VendorId} must be a Vendor or SalesRep (current role: {vendor.Role})"
            );
        }

        // Check if UPC Code is unique
        var existingProduct = await _context.Products.FirstOrDefaultAsync(
            p => p.UpcCode == request.UpcCode,
            cancellationToken
        );
        if (existingProduct != null)
        {
            throw new ValidationException(
                $"Product with UPC Code {request.UpcCode} already exists"
            );
        }

        // Check if Item Code is unique
        existingProduct = await _context.Products.FirstOrDefaultAsync(
            p => p.ItemCode == request.ItemCode,
            cancellationToken
        );
        if (existingProduct != null)
        {
            throw new ValidationException(
                $"Product with Item Code {request.ItemCode} already exists"
            );
        }

        var product = new Domain.Entities.Product
        {
            Name = request.Name,
            UpcCode = request.UpcCode,
            ItemCode = request.ItemCode,
            Description = request.Description,
            Size = request.Size,
            CasePack = request.CasePack,
            CasePrice = request.CasePrice,
            UnitPrice = request.UnitPrice,
            ImageUrl = request.ImageUrl,
            Stock = request.Stock,
            VendorId = request.VendorId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
