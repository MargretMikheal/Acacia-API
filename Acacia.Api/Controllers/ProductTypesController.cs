using Acacia.Api.ApiBases;
using Acacia.Core.Features.ProductTypes.Queries.GetAllProductTypes;
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

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllProductTypesQuery();
            var response = await _mediator.Send(query, cancellationToken);
            return NewResult(response);
        }
    }
}
