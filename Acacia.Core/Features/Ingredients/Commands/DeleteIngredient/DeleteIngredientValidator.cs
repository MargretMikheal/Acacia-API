using FluentValidation;

namespace Acacia.Core.Features.Ingredients.Commands.DeleteIngredient
{
    public class DeleteIngredientValidator : AbstractValidator<DeleteIngredientCommand>
    {
        public DeleteIngredientValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Ingredient Id is required.");
        }
    }
}
