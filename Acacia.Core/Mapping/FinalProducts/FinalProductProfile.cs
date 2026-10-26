using Acacia.Core.Features.FinalProducts;
using Acacia.Data.Entities;
using Acacia.Data.Entities.FinalProducts;
using AutoMapper;

namespace Acacia.Core.Mapping.FinalProducts;

public class FinalProductProfile : Profile
{
    public FinalProductProfile()
    {
        CreateMap<FinalProduct, FinalProductResponse>()
            .ForMember(dest => dest.OilName, opt => opt.MapFrom(src => src.Oil != null ? src.Oil.Name : string.Empty))
            .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType != null ? (src.ProductType.NameEn ?? src.ProductType.NameAr ?? string.Empty) : string.Empty))
            .ForMember(dest => dest.ProductSizeValue, opt => opt.MapFrom(src => src.ProductSize != null ? src.ProductSize.SizeValue : 0))
            .ForMember(dest => dest.BottleDesignName, opt => opt.MapFrom(src => src.BottleDesign != null ? src.BottleDesign.Name : string.Empty))
            .ForMember(dest => dest.BottleDesignImageUrl, opt => opt.MapFrom(src => src.BottleDesign != null ? src.BottleDesign.ImageUrl : null))
            .ForMember(dest => dest.AlcoholPercentage, opt => opt.MapFrom(src => src is PerfumeFinalProduct ? ((PerfumeFinalProduct)src).AlcoholPercentage : (decimal?)null))
            .ForMember(dest => dest.ProductTypeDiscriminator, opt => opt.MapFrom(src => src.GetType().Name.Replace("FinalProduct", "")));
    }
}
