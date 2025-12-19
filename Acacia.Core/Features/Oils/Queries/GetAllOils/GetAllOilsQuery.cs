using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Oils.Queries.GetAllOils
{
    public class GetAllOilsQuery : IRequest<Response<List<OilResponse>>>
    {
    }
}
