using Acacia.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommand : IRequest<Response<CompanyResponse>>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
