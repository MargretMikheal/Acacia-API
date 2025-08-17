using AutoMapper;

namespace Acacia.Core.Mapping.ProductSizes
{
    public partial class ProductSizeProfile : Profile
    {
        public ProductSizeProfile()
        {
            CreateProductSizeCommandMapping();
            ProductSizeResponseMapping();
        }
    }
}
