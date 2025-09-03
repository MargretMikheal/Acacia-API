using FluentValidation;

namespace Acacia.Core.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientValidator : AbstractValidator<CreateIngredientCommand>
    {
        public CreateIngredientValidator()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("Arabic name is required")
                .MaximumLength(100);

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("English name is required")
                .MaximumLength(100);

            RuleFor(x => x.Image)
                .NotNull().WithMessage("Image is required");
        }
    }
}
