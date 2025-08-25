using Acacia.Core.Features.PriceLists.Commands.UpdatePriceList;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.PriceLists;

public partial class PriceListProfile : Profile
{
    public void UpdatePriceListCommandMapping()
    {
        CreateMap<UpdatePriceListCommand, PriceList>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
