using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.FinalProducts.Queries.GetAllFinalProducts;

public class GetAllFinalProductsQuery : IRequest<Response<List<FinalProductResponse>>>
{
}
