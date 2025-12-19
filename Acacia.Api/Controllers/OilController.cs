using Acacia.Api.ApiBases;
using Acacia.Core.Features.Oils.Commands.CreateOil;
using Acacia.Core.Features.Oils.Commands.DeleteOil;
using Acacia.Core.Features.Oils.Commands.UpdateOil;
using Acacia.Core.Features.Oils.Queries.GetAllOils;
using Acacia.Core.Features.Oils.Queries.GetOilById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OilController : AppControllerBase
    {
        private readonly IMediator _mediator;

        public OilController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetOilByIdQuery { Id = id };
            var result = await _mediator.Send(query, cancellationToken);
            return NewResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllOilsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return NewResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOilCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return NewResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOilCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return NewResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteOilCommand { Id = id };
            var response = await _mediator.Send(command, cancellationToken);
            return NewResult(response);
        }
    }
}
