using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.ProductSizes.Commands.UpdateProductSize
{
    public class UpdateProductSizeCommand : IRequest<Response<ProductSizeResponse>>
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Size { get; set; }
    }
}
