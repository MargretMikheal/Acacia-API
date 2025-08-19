using AutoMapper;

namespace Acacia.Core.Mapping.ProductTypes
{
    public partial class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            GetByIdMapping();
        }
    }
}
