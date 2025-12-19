using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Ingredients.Queries.GetIngredientById
{
    public class GetIngredientByIdHandler : ResponseHandler,
        IRequestHandler<GetIngredientByIdQuery, Response<IngredientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetIngredientByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IStringLocalizer<SharedResources> localizer)
            : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<IngredientResponse>> Handle(GetIngredientByIdQuery request, CancellationToken cancellationToken)
        {
            var ingredient = await _unitOfWork.ingredientRepository.GetByIdAsync(request.Id, cancellationToken);
            if (ingredient == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Ingredient), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<IngredientResponse>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            var dto = _mapper.Map<IngredientResponse>(ingredient);
            return Success(dto);
        }
    }
}
