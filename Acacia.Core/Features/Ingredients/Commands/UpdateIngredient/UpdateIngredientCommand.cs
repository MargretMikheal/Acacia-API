using Acacia.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Features.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientCommand : IRequest<Response<IngredientResponse>>
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public IFormFile? Image { get; set; }
    }
}
