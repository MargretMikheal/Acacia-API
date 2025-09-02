using Acacia.Api.ApiBases;
using Acacia.Core.Features.BottleDesigns.Commands.CreateBottleDesign;
using Acacia.Core.Features.BottleDesigns.Commands.DeleteBottleDesign;
using Acacia.Core.Features.BottleDesigns.Commands.UpdateBottleDesign;
using Acacia.Core.Features.BottleDesigns.Queries.GetBottleDesignById;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BottleDesignsController : AppControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBottleDesign(
        [FromForm] CreateBottleDesignCommand command,
        CancellationToken token)
    {
        var response = await _mediator.Send(command, token);
        return NewResult(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateBottleDesign(
        [FromForm] UpdateBottleDesignCommand command,
        CancellationToken token)
    {
        var response = await _mediator.Send(command, token);
        return NewResult(response);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBottleDesign(
        int id,
        CancellationToken token)
    {
        var response = await _mediator.Send(new DeleteBottleDesignCommand(id), token);
        return NewResult(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBottleDesignById(int id,
        CancellationToken token)
    {
        var response = await _mediator.Send(new GetBottleDesignByIdQuery(id), token);
        return NewResult(response);
    }
}