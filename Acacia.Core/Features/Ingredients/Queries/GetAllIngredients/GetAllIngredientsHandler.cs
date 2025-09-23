using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Ingredients.Queries.GetAllIngredients
{
    public class GetAllIngredientsHandler : ResponseHandler,
        IRequestHandler<GetAllIngredientsQuery, Response<List<IngredientResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetAllIngredientsHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<List<IngredientResponse>>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await _unitOfWork.ingredientRepository.GetAllAsync(cancellationToken);

            var dto = _mapper.Map<List<IngredientResponse>>(ingredients);

            return Success(dto);
        }
    }
}
