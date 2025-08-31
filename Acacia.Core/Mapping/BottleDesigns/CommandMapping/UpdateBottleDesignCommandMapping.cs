using Acacia.Core.Features.BottleDesigns.Commands.UpdateBottleDesign;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.BottleDesigns;

public partial class BottleDesignProfile : Profile
{
    public void UpdateBottleDesignCommandMapping()
    {
        CreateMap<UpdateBottleDesignCommand, BottleDesign>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

    }
}
