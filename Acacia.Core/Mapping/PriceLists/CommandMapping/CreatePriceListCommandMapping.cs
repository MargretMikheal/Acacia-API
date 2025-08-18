using Acacia.Core.Features.PriceLists.Commands.CreatePriceList;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.PriceLists;

public partial class PriceListProfile : Profile
{
    public void CreatePriceListCommandMapping()
    {
        CreateMap<CreatePriceListCommand, PriceList>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }

    public void PriceListResponseMapping()
    {
        CreateMap<PriceList, PriceListResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}
