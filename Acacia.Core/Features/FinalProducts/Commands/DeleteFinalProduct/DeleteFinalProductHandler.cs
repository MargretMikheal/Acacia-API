using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.FinalProducts.Commands.DeleteFinalProduct;

public class DeleteFinalProductHandler : ResponseHandler,
    IRequestHandler<DeleteFinalProductCommand, Response<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public DeleteFinalProductHandler(
        IUnitOfWork unitOfWork,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<Response<string>> Handle(DeleteFinalProductCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.finalProductRepository.ExistsAsync(request.Id, cancellationToken);
        if (!exists)
        {
            var errors = new Dictionary<string, List<string>>
            {
                ["FinalProduct"] = new List<string> { _localizer[SharedResourcesKeys.NotFound] }
            };
            return NotFound<string>(_localizer[SharedResourcesKeys.NotFound], errors);
        }

        await _unitOfWork.finalProductRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Deleted<string>();
    }
}
