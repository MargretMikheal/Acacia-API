using FluentValidation;

namespace Acacia.Core.Features.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientValidator : AbstractValidator<UpdateIngredientCommand>
    {
        public UpdateIngredientValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Ingredient Id is required.");

            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("Arabic name is required.")
                .MaximumLength(200);

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("English name is required.")
                .MaximumLength(200);
        }
    }
}
