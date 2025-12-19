using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.BottleDesigns.Queries.GetBottleDesignById;

public class GetBottleDesignByIdQuery : IRequest<Response<BottleDesignResponse>>
{
    public int Id { get; set; }
    public GetBottleDesignByIdQuery(int id)
    {
        Id = id;
    }
}
