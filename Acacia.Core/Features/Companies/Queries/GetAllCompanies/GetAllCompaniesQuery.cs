using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Companies.Queries.GetAllCompanies
{
    public class GetAllCompaniesQuery : IRequest<Response<List<CompanyResponse>>>
    {
    }
}
