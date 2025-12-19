using Acacia.Core.Features.Ingredients;
using Acacia.Data.Entities;

namespace Acacia.Core.Mapping.Ingredients
{
    public partial class IngredientProfile
    {
        public void GetIngredientMapping()
        {
            CreateMap<Ingredient, IngredientResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        }
    }
}
