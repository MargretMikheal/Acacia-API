using FluentValidation;

namespace Acacia.Core.Features.BottleDesigns.Commands.CreateBottleDesign;

public class CreateBottleDesignValidator : AbstractValidator<CreateBottleDesignCommand>
{
    public CreateBottleDesignValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.ProductTypeId)
            .GreaterThan(0).WithMessage("ProductTypeId must be greater than zero.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to zero.");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image file is required.")
            .Must(file => file.Length > 0).WithMessage("Image file must not be empty.")
            .Must(file => file.ContentType.StartsWith("image/")).WithMessage("File must be an image.");
    }
}
