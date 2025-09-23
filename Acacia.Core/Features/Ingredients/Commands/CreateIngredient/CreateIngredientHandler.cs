using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Ingredients.Commands.CreateIngredient
{
    public class CreateIngredientHandler : ResponseHandler,
        IRequestHandler<CreateIngredientCommand, Response<IngredientResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICloudinaryService _fileService;

        public CreateIngredientHandler(
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

        public async Task<Response<IngredientResponse>> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            var exists = await _unitOfWork.ingredientRepository
                .AnyAsync(x => x.NameAr == request.NameAr || x.NameEn == request.NameEn, cancellationToken);

            if (exists)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Ingredient), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
                };
                return UnprocessableEntity<IngredientResponse>(error);
            }

            var entity = _mapper.Map<Ingredient>(request);

            if (request.Image != null)
            {
                var uploadResult = await _fileService.UploadImageAsync(request.Image, "Ingredients", cancellationToken);
                entity.ImageUrl = uploadResult.Url;
                entity.ImagePublicId = uploadResult.PublicId;
            }

            var created = await _unitOfWork.ingredientRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<IngredientResponse>(created);
            return Created(dto);
        }
    }
}
