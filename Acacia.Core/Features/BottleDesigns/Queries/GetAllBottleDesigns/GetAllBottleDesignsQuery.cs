using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.BottleDesigns.Queries.GetAllBottleDesigns;

public class GetAllBottleDesignsQuery : IRequest<Response<List<BottleDesignResponse>>>
{
}
