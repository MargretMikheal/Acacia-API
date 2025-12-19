using Acacia.Core.Features.Oils;
using Acacia.Core.Features.Oils.Commands.CreateOil;
using Acacia.Core.Features.Oils.Commands.UpdateOil;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.Oils
{
    public partial class OilProfile : Profile
    {
        public OilProfile()
        {
            CreateMap<CreateOilCommand, Oil>()
                .ForMember(dest => dest.OilIngredients, opt => opt.Ignore())
                .ForMember(dest => dest.SeasonScore, opt => opt.Ignore())
                .ForMember(dest => dest.OccasionScore, opt => opt.Ignore());

            CreateMap<Oil, OilResponse>()
                .ForMember(dest => dest.IngredientIds, opt => opt.MapFrom(src => src.OilIngredients.Select(oi => oi.IngredientId)));
            CreateMap<UpdateOilCommand, Oil>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
