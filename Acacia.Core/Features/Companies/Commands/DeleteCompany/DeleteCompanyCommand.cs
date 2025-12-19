using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
