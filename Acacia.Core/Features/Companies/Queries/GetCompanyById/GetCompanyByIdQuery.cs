using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<Response<CompanyResponse>>
    {
        public int Id { get; set; }
    }
}
