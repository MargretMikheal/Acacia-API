using Acacia.Api.ApiBases;
using Acacia.Core.Features.ProductSizes.Commands.CreateProductSize;
using Acacia.Core.Features.ProductSizes.Commands.DeleteProductSize;
using Acacia.Core.Features.ProductSizes.Commands.UpdateProductSize;
using Acacia.Core.Features.ProductSizes.Queries.GetAllProductSizes;
using Acacia.Core.Features.ProductSizes.Queries.GetProductSizeById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSizesController : AppControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] CreateProductSizeCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update([FromBody] UpdateProductSizeCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return NewResult(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteProductSizeCommand(id));
            return NewResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _mediator.Send(new GetProductSizeByIdQuery(id));
            return NewResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllProductSizesQuery(), cancellationToken);
            return NewResult(response);
        }
    }
}
