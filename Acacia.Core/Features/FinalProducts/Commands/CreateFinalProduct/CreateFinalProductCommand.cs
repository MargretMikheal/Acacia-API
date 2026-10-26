using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.FinalProducts.Commands.CreateFinalProduct;

public class CreateFinalProductCommand : IRequest<Response<FinalProductResponse>>
{
    public int OilId { get; set; }
    public int ProductTypeId { get; set; }
    public int ProductSizeId { get; set; }
    public int BottleDesignId { get; set; }
    public decimal BasePrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? AlcoholPercentage { get; set; }
}
