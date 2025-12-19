using Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById;
using Acacia.Data.Entities;
using AutoMapper;
namespace Acacia.Core.Mapping.ProductTypes
{
    public partial class ProductTypeProfile : Profile
    {
        void GetByIdMapping()
        {
            CreateMap<ProductType, ProductTypeResponse>()
                .ForMember(
                    dest => dest.Name,
                    memberOptions => memberOptions.MapFrom(src => src.Localize(src.NameAr, src.NameEn))
                );
        }
    }
}
