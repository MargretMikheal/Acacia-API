using Acacia.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Acacia.Core.Features.BottleDesigns.Commands.UpdateBottleDesign;

public class UpdateBottleDesignCommand : IRequest<Response<BottleDesignResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public decimal Price { get; set; }
    // Image file for the bottle design
    public IFormFile? Image { get; set; }
}
