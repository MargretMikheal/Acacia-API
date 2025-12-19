using AutoMapper;

namespace Acacia.Core.Mapping.Ingredients
{
    public partial class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateIngredientMapping();
            GetIngredientMapping();
            UpdateIngredientMapping();
        }
    }
}
