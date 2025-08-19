using Acacia.Core.Features.ProductSizes.Commands.UpdateProductSize;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.ProductSizes
{
    public partial class ProductSizeProfile : Profile
    {
        public void UpdateProductSizeMapping()
        {
            CreateMap<UpdateProductSizeCommand, ProductSize>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
