using Acacia.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Features.BottleDesigns.Commands.CreateBottleDesign;

public class CreateBottleDesignCommand : IRequest<Response<BottleDesignResponse>>
{
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public decimal Price { get; set; }

    // Image file for the bottle design
    public IFormFile Image { get; set; }
}
