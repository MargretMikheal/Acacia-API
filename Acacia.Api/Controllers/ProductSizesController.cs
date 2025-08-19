using Acacia.Api.ApiBases;
using Acacia.Core.Features.ProductSizes.Commands.CreateProductSize;
using Acacia.Core.Features.ProductSizes.Commands.DeleteProductSize;
using Acacia.Core.Features.ProductSizes.Commands.UpdateProductSize;
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
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductSizeCommand command, CancellationToken cancellationToken)
        {

            var response = await _mediator.Send(command, cancellationToken);
            return NewResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteProductSizeCommand(id));
            return NewResult(response);
        }
    }
}
