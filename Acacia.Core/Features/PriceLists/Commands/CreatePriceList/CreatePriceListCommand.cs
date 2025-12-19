using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.PriceLists.Commands.CreatePriceList;

public class CreatePriceListCommand : IRequest<Response<PriceListResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
