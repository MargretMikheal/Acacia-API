using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Ingredients.Queries.GetIngredientById
{
    public class GetIngredientByIdQuery : IRequest<Response<IngredientResponse>>
    {
        public int Id { get; set; }
    }
}
