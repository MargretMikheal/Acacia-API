using AutoMapper;

namespace Acacia.Core.Mapping.Companies
{
    public partial class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateCompanyMapping();
            UpdateCompanyMapping();
            GetCompanyMapping();
        }
    }
}
