using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.ProductSizes.Queries.GetProductSizeById
{
    public class GetProductSizeByIdQuery : IRequest<Response<ProductSizeResponse>>
    {
        public int Id { get; set; }

        public GetProductSizeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
