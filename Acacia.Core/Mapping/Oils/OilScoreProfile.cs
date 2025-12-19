using Acacia.Core.Features.Oils;
using Acacia.Core.Features.Oils.Commands.CreateOil;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.Oils
{
    public partial class OilScoreProfile : Profile
    {
        public OilScoreProfile()
        {
            CreateMap<OilSeasonScoreRequest, OilSeasonScore>();
            CreateMap<OilOccasionScoreRequest, OilOccasionScore>();

            CreateMap<OilSeasonScore, OilSeasonScoreResponse>();
            CreateMap<OilOccasionScore, OilOccasionScoreResponse>();
        }
    }
}
