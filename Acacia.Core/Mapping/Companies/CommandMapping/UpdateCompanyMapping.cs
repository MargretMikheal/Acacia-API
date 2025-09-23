using Acacia.Core.Features.Companies.Commands.UpdateCompany;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.Companies
{
    public partial class CompanyProfile : Profile
    {
        public void UpdateCompanyMapping()
        {
            CreateMap<UpdateCompanyCommand, Company>();
        }
    }
}
