using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.FinalProducts.Queries.GetFinalProductById;

public class GetFinalProductByIdQuery : IRequest<Response<FinalProductResponse>>
{
    public int Id { get; set; }

    public GetFinalProductByIdQuery(int id)
    {
        Id = id;
    }
}
