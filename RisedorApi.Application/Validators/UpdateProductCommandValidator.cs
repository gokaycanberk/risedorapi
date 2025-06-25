using FluentValidation;
using RisedorApi.Application.Commands.Product;

namespace RisedorApi.Application.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Invalid Product ID");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.UpcCode)
            .NotEmpty()
            .WithMessage("UPC Code is required")
            .MaximumLength(20)
            .WithMessage("UPC Code must not exceed 20 characters");

        RuleFor(x => x.ItemCode)
            .NotEmpty()
            .WithMessage("Item Code is required")
            .MaximumLength(20)
            .WithMessage("Item Code must not exceed 20 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters");

        RuleFor(x => x.Size)
            .NotEmpty()
            .WithMessage("Size is required")
            .MaximumLength(20)
            .WithMessage("Size must not exceed 20 characters");

        RuleFor(x => x.CasePack).GreaterThan(0).WithMessage("Case Pack must be greater than 0");

        RuleFor(x => x.CasePrice).GreaterThan(0).WithMessage("Case Price must be greater than 0");

        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Unit Price must be greater than 0");

        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative");

        When(
            x => x.ImageUrl != null,
            () =>
            {
                RuleFor(x => x.ImageUrl)
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                    .WithMessage("Image URL must be a valid URL");
            }
        );
    }
}
