using Acacia.Core.Features.Ingredients.Commands.UpdateIngredient;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.Ingredients
{
    public partial class IngredientProfile : Profile
    {
        public void UpdateIngredientMapping()
        {
            CreateMap<UpdateIngredientCommand, Ingredient>();
        }
    }
}
