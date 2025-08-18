using AutoMapper;

namespace Acacia.Core.Mapping.PriceLists;

public partial class PriceListProfile : Profile
{
    public PriceListProfile()
    {
        CreatePriceListCommandMapping();
        PriceListResponseMapping();
    }
}
