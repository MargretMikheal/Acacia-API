using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.ProductSizes.Commands.CreateProductSize
{
    public class CreateProductSizeCommand : IRequest<Response<ProductSizeResponse>>
    {
        public int ProductTypeId { get; set; }
        public decimal Size { get; set; }
    }
}
