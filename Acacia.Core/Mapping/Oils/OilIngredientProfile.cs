using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.Oils
{
    public partial class OilIngredientProfile : Profile
    {
        public OilIngredientProfile()
        {
            CreateMap<int, OilIngredient>()
                .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src));
        }
    }
}
