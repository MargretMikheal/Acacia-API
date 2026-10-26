using Acacia.Api.ApiBases;
using Acacia.Core.Features.PriceLists.Commands.CreatePriceList;
using Acacia.Core.Features.PriceLists.Commands.DeletePriceList;
using Acacia.Core.Features.PriceLists.Commands.UpdatePriceList;
using Acacia.Core.Features.PriceLists.Queries.GetAllPriceLists;
using Acacia.Core.Features.PriceLists.Queries.GetPriceListById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PriceListController : AppControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreatePriceList([FromBody] CreatePriceListCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);
        return NewResult(response);
    }

    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdatePriceList([FromBody] UpdatePriceListCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);
        return NewResult(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeletePriceList(int id, CancellationToken token)
    {
        var response = await _mediator.Send(new DeletePriceListCommand(id), token);
        return NewResult(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        var response = await _mediator.Send(new GetPriceListByIdQuery(id), token);
        return NewResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var response = await _mediator.Send(new GetAllPriceListsQuery(), token);
        return NewResult(response);
    }
}
