using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.BottleDesigns.Commands.DeleteBottleDesign;

public class DeleteBottleDesignCommand : IRequest<Response<string>>
{
    public int Id { get; set; }

    public DeleteBottleDesignCommand(int id)
    {
        Id = id;
    }
}
