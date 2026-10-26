using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.FinalProducts.Commands.DeleteFinalProduct;

public class DeleteFinalProductCommand : IRequest<Response<string>>
{
    public int Id { get; set; }

    public DeleteFinalProductCommand(int id)
    {
        Id = id;
    }
}
