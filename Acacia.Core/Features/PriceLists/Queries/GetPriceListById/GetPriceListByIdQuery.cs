using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.PriceLists.Queries.GetPriceListById;

public class GetPriceListByIdQuery : IRequest<Response<PriceListResponse>>
{
    public int Id { get; set; }
    public GetPriceListByIdQuery(int id)
    {
        Id = id;
    }
}
