using Acacia.Api.ApiBases;
using Acacia.Core.Features.Ingredients.Commands.CreateIngredient;
using Acacia.Core.Features.Ingredients.Commands.DeleteIngredient;
using Acacia.Core.Features.Ingredients.Commands.UpdateIngredient;
using Microsoft.AspNetCore.Mvc;

namespace Acacia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : AppControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateIngredientCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateIngredientCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteIngredientCommand { Id = id };
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
    }
}
