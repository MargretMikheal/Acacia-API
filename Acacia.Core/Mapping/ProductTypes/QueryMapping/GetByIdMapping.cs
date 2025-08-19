using Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById;
using Acacia.Data.Entities;
using AutoMapper;
namespace Acacia.Core.Mapping.ProductTypes
{
    public partial class ProductTypeProfile : Profile
    {
        void GetByIdMapping()
        {
            CreateMap<ProductType, ProductTypeResponse>();
        }
    }
}
