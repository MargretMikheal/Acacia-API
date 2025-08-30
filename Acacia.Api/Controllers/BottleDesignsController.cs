using Acacia.Api.ApiBases;
using Acacia.Core.Features.BottleDesigns.Commands.CreateBottleDesign;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BottleDesignsController : AppControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateBottleDesign(
        [FromForm] CreateBottleDesignCommand command,
        CancellationToken token)
    {
        var response = await _mediator.Send(command, token);
        return NewResult(response);
    }
}