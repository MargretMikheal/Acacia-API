using Acacia.Core.Features.BottleDesigns;
using Acacia.Core.Features.BottleDesigns.Commands.CreateBottleDesign;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.BottleDesigns;

public partial class BottleDesignProfile : Profile
{
    public void CreateBottleDesignCommandMapping()
    {
        CreateMap<CreateBottleDesignCommand, BottleDesign>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.ProductTypeId))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
    }

    public void BottleDesignResponseMapping()
    {
        CreateMap<BottleDesign, BottleDesignResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.ProductTypeId))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
    }
}
