using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.ProductSizes.Commands.DeleteProductSize
{
    public class DeleteProductSizeCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteProductSizeCommand(int id)
        {
            Id = id;
        }
    }
}
