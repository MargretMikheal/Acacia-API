using Acacia.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientCommand : IRequest<Response<IngredientResponse>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public IFormFile Image { get; set; }
    }
}
