using Acacia.Core.Bases;
using MediatR;

namespace Acacia.Core.Features.Oils.Commands.DeleteOil;

public class DeleteOilCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
}
