using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Oils.Commands.CreateOil
{
    public class CreateOilHandler : ResponseHandler,
        IRequestHandler<CreateOilCommand, Response<OilResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public CreateOilHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<OilResponse>> Handle(CreateOilCommand request, CancellationToken cancellationToken)
        {
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

            var entity = _mapper.Map<Oil>(request);

            if (request.IngredientIds != null && request.IngredientIds.Any())
            {
                entity.OilIngredients = request.IngredientIds
                    .Select(id => new OilIngredient { IngredientId = id })
                    .ToList();
            }

            if (request.SeasonScore != null)
                entity.SeasonScore = _mapper.Map<OilSeasonScore>(request.SeasonScore);

            if (request.OccasionScore != null)
                entity.OccasionScore = _mapper.Map<OilOccasionScore>(request.OccasionScore);

            var created = await _unitOfWork.oilRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<OilResponse>(created);

            return Created(dto);
        }
    }
}
