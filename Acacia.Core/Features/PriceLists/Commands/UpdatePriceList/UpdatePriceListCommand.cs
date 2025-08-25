using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.PriceLists.Commands.UpdatePriceList;

public class UpdatePriceListCommand : IRequest<Response<PriceListResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
