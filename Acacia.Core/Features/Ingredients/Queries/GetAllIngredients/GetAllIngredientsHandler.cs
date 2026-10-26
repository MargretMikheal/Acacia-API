using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
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
        private readonly IStorageService _storageService;

        public GetAllIngredientsHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer,
            IStorageService storageService
        ) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _storageService = storageService;
        }

        public async Task<Response<List<IngredientResponse>>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await _unitOfWork.ingredientRepository.GetAllAsync(cancellationToken);

            var dto = _mapper.Map<List<IngredientResponse>>(ingredients);
            foreach (var ing in dto)
            {
                ing.ImageUrl = _storageService.ResolvePresignedUrl(ing.ImageUrl) ?? ing.ImageUrl;
            }

            return Success(dto);
        }
    }
}
