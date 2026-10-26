using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.ProductSizes.Queries.GetAllProductSizes;

public class GetAllProductSizesQuery : IRequest<Response<List<ProductSizeResponse>>>
{
}
