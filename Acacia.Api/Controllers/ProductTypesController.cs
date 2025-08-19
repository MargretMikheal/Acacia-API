using Acacia.Api.ApiBases;
using Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesController : AppControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetProductTypeByIdQuery { Id = id };
            var response = await _mediator.Send(query, cancellationToken);
            return NewResult(response);
        }
    }
}
