using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Ingredients.Queries.GetAllIngredients
{
    public class GetAllIngredientsQuery : IRequest<Response<List<IngredientResponse>>>
    {
    }
}
