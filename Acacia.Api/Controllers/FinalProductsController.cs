using Acacia.Api.ApiBases;
using Acacia.Core.Features.FinalProducts.Commands.CreateFinalProduct;
using Acacia.Core.Features.FinalProducts.Commands.DeleteFinalProduct;
using Acacia.Core.Features.FinalProducts.Queries.GetAllFinalProducts;
using Acacia.Core.Features.FinalProducts.Queries.GetFinalProductById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinalProductsController : AppControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var response = await _mediator.Send(new GetAllFinalProductsQuery(), token);
        return NewResult(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        var response = await _mediator.Send(new GetFinalProductByIdQuery(id), token);
        return NewResult(response);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Create([FromBody] CreateFinalProductCommand command, CancellationToken token)
    {
        var response = await _mediator.Send(command, token);
        return NewResult(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Delete(int id, CancellationToken token)
    {
        var response = await _mediator.Send(new DeleteFinalProductCommand(id), token);
        return NewResult(response);
    }
}
