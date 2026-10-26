using FluentValidation;

namespace Acacia.Core.Features.FinalProducts.Commands.CreateFinalProduct;

public class CreateFinalProductValidator : AbstractValidator<CreateFinalProductCommand>
{
    public CreateFinalProductValidator()
    {
        RuleFor(x => x.OilId)
            .GreaterThan(0).WithMessage("Valid Fragrance Oil ID is required.");

        RuleFor(x => x.ProductTypeId)
            .GreaterThan(0).WithMessage("Valid Product Type ID is required.");

        RuleFor(x => x.ProductSizeId)
            .GreaterThan(0).WithMessage("Valid Product Size ID is required.");

        RuleFor(x => x.BottleDesignId)
            .GreaterThan(0).WithMessage("Valid Bottle Design ID is required.");

        RuleFor(x => x.BasePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Base price cannot be negative.");

        RuleFor(x => x.TotalPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Total price cannot be negative.");

        RuleFor(x => x.AlcoholPercentage)
            .InclusiveBetween(0, 100)
            .When(x => x.AlcoholPercentage.HasValue)
            .WithMessage("Alcohol percentage must be between 0 and 100.");
    }
}
