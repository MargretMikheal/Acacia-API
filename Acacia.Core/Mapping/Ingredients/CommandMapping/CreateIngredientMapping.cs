using Acacia.Core.Features.Ingredients.Commands.CreateIngredient;
using Acacia.Data.Entities;

namespace Acacia.Core.Mapping.Ingredients
{
    public partial class IngredientProfile
    {
        public void CreateIngredientMapping()
        {
            CreateMap<CreateIngredientCommand, Ingredient>();
        }
    }
}
