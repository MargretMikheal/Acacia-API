using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Oils.Commands.UpdateOil;

public class UpdateOilHandler : ResponseHandler,
    IRequestHandler<UpdateOilCommand, Response<OilResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public UpdateOilHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<SharedResources> localizer
    ) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Response<OilResponse>> Handle(UpdateOilCommand request, CancellationToken cancellationToken)
    {
        var oil = await _unitOfWork.oilRepository.GetByIdWithIncludesAsync(request.Id, cancellationToken);
        if (oil is null)
        {
            var error = new Dictionary<string, List<string>>
            {
                { nameof(Oil), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
            };
            return NotFound<OilResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        var companyExists = await _unitOfWork.companyRepository.GetByIdAsync(request.CompanyId, cancellationToken);
        if (companyExists is null)
        {
            var error = new Dictionary<string, List<string>>
            {
                { nameof(Oil.CompanyId), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
            };
            return NotFound<OilResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        var priceListExists = await _unitOfWork.priceListRepository.GetByIdAsync(request.PriceListId, cancellationToken);
        if (priceListExists is null)
        {
            var error = new Dictionary<string, List<string>>
            {
                { nameof(Oil.PriceListId), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
            };
            return NotFound<OilResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        if (request.IngredientIds != null && request.IngredientIds.Any())
        {
            var existingIngredients = await _unitOfWork.ingredientRepository
                .FindAllAsync(x => request.IngredientIds.Contains(x.Id), cancellationToken);

            if (existingIngredients.Count() != request.IngredientIds.Count)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Oil.OilIngredients), new List<string> { _localizer["Some ingredients not found"] } }
                };
                return UnprocessableEntity<OilResponse>(error);
            }
        }

        _mapper.Map(request, oil);

        oil.OilIngredients.Clear();
        if (request.IngredientIds != null && request.IngredientIds.Any())
        {
            oil.OilIngredients = request.IngredientIds
                .Select(id => new OilIngredient { IngredientId = id, OilId = oil.Id })
                .ToList();
        }

        if (request.SeasonScore != null)
        {
            if (oil.SeasonScore == null)
                oil.SeasonScore = new OilSeasonScore { OilId = oil.Id };

            _mapper.Map(request.SeasonScore, oil.SeasonScore);
        }

        if (request.OccasionScore != null)
        {
            if (oil.OccasionScore == null)
                oil.OccasionScore = new OilOccasionScore { OilId = oil.Id };

            _mapper.Map(request.OccasionScore, oil.OccasionScore);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<OilResponse>(oil);
        return Success(dto);
    }
}
