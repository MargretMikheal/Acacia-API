using Acacia.Api.ApiBases;
using Acacia.Core.Features.ProductSizes.Commands.CreateProductSize;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : AppControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductSizeCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
