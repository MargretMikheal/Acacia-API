using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.PriceLists.Queries.GetAllPriceLists;

public class GetAllPriceListsQuery : IRequest<Response<List<PriceListResponse>>>
{
}
