using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Ingredients.Commands.DeleteIngredient
{
    public class DeleteIngredientCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
