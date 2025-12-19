using Acacia.Core.Features.Companies.Commands.CreateCompany;
using Acacia.Data.Entities;
using AutoMapper;

namespace Acacia.Core.Mapping.Companies
{
    public partial class CompanyProfile : Profile
    {
        private void CreateCompanyMapping()
        {
            CreateMap<CreateCompanyCommand, Company>();
        }
    }
}
