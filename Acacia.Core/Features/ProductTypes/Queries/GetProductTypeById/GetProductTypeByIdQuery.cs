using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById
{
    public class GetProductTypeByIdQuery : IRequest<Response<ProductTypeResponse>>
    {
        public int Id { get; set; }
    }
}
