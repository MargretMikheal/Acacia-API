using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Oils.Commands.DeleteOil;

public class DeleteOilHandler : ResponseHandler,
    IRequestHandler<DeleteOilCommand, Response<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public DeleteOilHandler(
        IUnitOfWork unitOfWork,
        IStringLocalizer<SharedResources> localizer
    ) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<Response<string>> Handle(DeleteOilCommand request, CancellationToken cancellationToken)
    {
        var oil = await _unitOfWork.oilRepository.GetByIdWithIncludesAsync(request.Id, cancellationToken);

        if (oil is null)
        {
            var error = new Dictionary<string, List<string>>
            {
                { nameof(Oil), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
            };
            return NotFound<string>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        if (oil.OilIngredients?.Any() == true)
            await _unitOfWork.oilRepository.RemoveOilIngredientsAsync(oil, cancellationToken);


        if (oil.SeasonScore != null)
            await _unitOfWork.oilSeasonRepository.DeleteAsync(oil.SeasonScore.Id);

        if (oil.OccasionScore != null)
            await _unitOfWork.oilOccasionRepository.DeleteAsync(oil.OccasionScore.Id);

        await _unitOfWork.oilRepository.DeleteAsync(oil.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Deleted<string>();
    }
}
