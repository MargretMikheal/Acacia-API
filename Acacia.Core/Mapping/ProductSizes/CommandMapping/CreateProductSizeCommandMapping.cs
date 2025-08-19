using Acacia.Core.Features.ProductSizes;
using Acacia.Core.Features.ProductSizes.Commands.CreateProductSize;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.ProductSizes
{
    public partial class ProductSizeProfile : Profile
    {
        public void CreateProductSizeCommandMapping()
        {
            CreateMap<CreateProductSizeCommand, ProductSize>()
                .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.ProductTypeId))
                .ForMember(dest => dest.SizeValue, opt => opt.MapFrom(src => src.Size));
        }

        public void ProductSizeResponseMapping()
        {
            CreateMap<ProductSize, ProductSizeResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.ProductTypeId))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.SizeValue));
        }
    }
}
