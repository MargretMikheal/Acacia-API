using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.PriceLists.Commands.DeletePriceList;

public class DeletePriceListCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public DeletePriceListCommand(int id)
    {
        Id = id;
    }
}
