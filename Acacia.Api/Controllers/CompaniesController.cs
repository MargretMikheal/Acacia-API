using Acacia.Api.ApiBases;
using Acacia.Core.Features.Companies.Commands.CreateCompany;
using Acacia.Core.Features.Companies.Commands.DeleteCompany;
using Acacia.Core.Features.Companies.Commands.UpdateCompany;
using Acacia.Core.Features.Companies.Queries.GetAllCompanies;
using Acacia.Core.Features.Companies.Queries.GetCompanyById;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCompanyCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCompanyCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id in URL and body do not match");

            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteCompanyCommand { Id = id };
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => NewResult(await _mediator.Send(new GetCompanyByIdQuery { Id = id }));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => NewResult(await _mediator.Send(new GetAllCompaniesQuery()));
    }
}
