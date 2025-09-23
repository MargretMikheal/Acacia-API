using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Oils.Queries.GetOilById
{
    public class GetOilByIdQuery : IRequest<Response<OilResponse>>
    {
        public int Id { get; set; }
    }
}
