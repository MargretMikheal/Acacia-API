using Acacia.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IRequest<Response<CompanyResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
