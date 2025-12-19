using Acacia.Core.Features.Companies;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.Companies
{
    public partial class CompanyProfile : Profile
    {
        private void GetCompanyMapping()
        {
            CreateMap<Company, CompanyResponse>();
        }

    }
}
