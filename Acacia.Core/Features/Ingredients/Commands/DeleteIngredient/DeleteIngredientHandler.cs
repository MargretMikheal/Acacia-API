using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Ingredients.Commands.DeleteIngredient
{
    public class DeleteIngredientHandler : ResponseHandler,
        IRequestHandler<DeleteIngredientCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICloudinaryService _fileService;

        public DeleteIngredientHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer,
            ICloudinaryService fileService) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _fileService = fileService;
        }

        public async Task<Response<string>> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = await _unitOfWork.ingredientRepository.GetByIdAsync(request.Id, cancellationToken);
            if (ingredient == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Ingredient), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            if (!string.IsNullOrEmpty(ingredient.ImagePublicId))
            {
                await _fileService.DeleteImageAsync(ingredient.ImagePublicId, cancellationToken);
            }

            await _unitOfWork.ingredientRepository.DeleteAsync(ingredient.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success<string>(_localizer[SharedResourcesKeys.Deleted]);
        }
    }
}
