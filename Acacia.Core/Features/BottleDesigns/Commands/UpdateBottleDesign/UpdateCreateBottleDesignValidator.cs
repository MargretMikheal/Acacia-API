using FluentValidation;

namespace Acacia.Core.Features.BottleDesigns.Commands.UpdateBottleDesign;

public class UpdateCreateBottleDesignValidator : AbstractValidator<UpdateBottleDesignCommand>
{
    public UpdateCreateBottleDesignValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Invalid BottleDesign Id");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.ProductTypeId)
            .NotEmpty().WithMessage("ProductTypeId is required.")
            .GreaterThan(0).WithMessage("Invalid ProductTypeId.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Image)
            .Must(file => file == null || (file.Length > 0 && (file.ContentType == "image/jpeg" || file.ContentType == "image/png")))
            .WithMessage("Image must be a valid JPEG or PNG file.");
    }
}
