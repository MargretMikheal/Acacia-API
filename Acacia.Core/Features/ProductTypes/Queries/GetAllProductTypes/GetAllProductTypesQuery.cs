using Acacia.Core.Bases;
using Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById;
using MediatR;

namespace Acacia.Core.Features.ProductTypes.Queries.GetAllProductTypes;

public class GetAllProductTypesQuery : IRequest<Response<List<ProductTypeResponse>>>
{
}
