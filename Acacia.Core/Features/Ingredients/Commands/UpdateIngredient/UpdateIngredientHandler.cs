using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Ingredients.Commands.UpdateIngredient
{
    public class UpdateIngredientHandler : ResponseHandler,
        IRequestHandler<UpdateIngredientCommand, Response<IngredientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICloudinaryService _fileService;

        public UpdateIngredientHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer,
            ICloudinaryService fileService) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _fileService = fileService;
        }

        public async Task<Response<IngredientResponse>> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
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

            var exists = await _unitOfWork.ingredientRepository
                .AnyAsync(x => (x.NameAr == request.NameAr || x.NameEn == request.NameEn) && x.Id != request.Id, cancellationToken);

            if (exists)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Ingredient), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
                };
                return UnprocessableEntity<IngredientResponse>(error);
            }

            ingredient.NameAr = request.NameAr;
            ingredient.NameEn = request.NameEn;

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(ingredient.ImagePublicId))
                {
                    await _fileService.DeleteImageAsync(ingredient.ImagePublicId, cancellationToken);
                }

                var uploadResult = await _fileService.UploadImageAsync(request.Image, "Ingredients", cancellationToken);
                ingredient.ImageUrl = uploadResult.Url;
                ingredient.ImagePublicId = uploadResult.PublicId;
            }

            await _unitOfWork.ingredientRepository.UpdateAsync(ingredient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<IngredientResponse>(ingredient);
            return Success(dto);
        }
    }
}
