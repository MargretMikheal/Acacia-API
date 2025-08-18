using Acacia.Api.ApiBases;
using Acacia.Core.Features.PriceLists.Commands.CreatePriceList;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PriceListController : AppControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePriceList([FromBody] CreatePriceListCommand command)
    {
        var response = await _mediator.Send(command);
        return NewResult(response);
    }
}
