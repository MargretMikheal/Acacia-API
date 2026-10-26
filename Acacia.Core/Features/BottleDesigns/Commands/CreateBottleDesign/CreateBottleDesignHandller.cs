using Acacia.Core.Bases;
using Acacia.Core.Features.PriceLists;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.BottleDesigns.Commands.CreateBottleDesign;

public class CreateBottleDesignHandller : ResponseHandler,
        IRequestHandler<CreateBottleDesignCommand, Response<BottleDesignResponse>>
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly ICloudinaryService _fileService;
    #endregion

    #region Ctor
    public CreateBottleDesignHandller(
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
    #endregion

    #region Methods
    public async Task<Response<BottleDesignResponse>> Handle(CreateBottleDesignCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.productTypeRepository.ExistsAsync(request.ProductTypeId, cancellationToken);
        if (!exists)
        {
            var error = new Dictionary<string, List<string>>
                {
                    { nameof(BottleDesign), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
            return NotFound<BottleDesignResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        var entity = _mapper.Map<BottleDesign>(request);

        string? uploadedPublicId = null;
        try
        {
            // Upload image to Backblaze B2/storage
            var uploadResult = await _fileService.UploadImageAsync(request.Image, "Bottle_Design", cancellationToken);
            uploadedPublicId = uploadResult.PublicId;

            entity.ImageUrl = uploadResult.Url;
            entity.ImagePublicId = uploadResult.PublicId;

            var created = await _unitOfWork.bottleDesignRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<BottleDesignResponse>(created);
            return Created(dto);
        }
        catch (Exception)
        {
            // Compensating action: remove uploaded file if persistence fails
            if (!string.IsNullOrEmpty(uploadedPublicId))
            {
                await _fileService.DeleteImageAsync(uploadedPublicId, cancellationToken);
            }
            throw;
        }
    }
    #endregion
}
