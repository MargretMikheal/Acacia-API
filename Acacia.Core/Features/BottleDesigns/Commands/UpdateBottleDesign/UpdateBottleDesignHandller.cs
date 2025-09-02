using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.BottleDesigns.Commands.UpdateBottleDesign;

public class UpdateBottleDesignHandller : ResponseHandler,
    IRequestHandler<UpdateBottleDesignCommand, Response<BottleDesignResponse>>
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly ICloudinaryService _fileService;
    #endregion

    #region Ctor
    public UpdateBottleDesignHandller(
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
    public async Task<Response<BottleDesignResponse>> Handle(UpdateBottleDesignCommand request, CancellationToken cancellationToken)
    {

        var existing = await _unitOfWork.bottleDesignRepository.GetByIdAsync(request.Id);
        if (existing == null)
        {
            var error = new Dictionary<string, List<string>>
                {
                    { nameof(BottleDesign), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
            return NotFound<BottleDesignResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        var exists = await _unitOfWork.productTypeRepository.ExistsAsync(request.ProductTypeId, cancellationToken);
        if (!exists)
        {
            var error = new Dictionary<string, List<string>>
                {
                    { nameof(BottleDesign), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
            return NotFound<BottleDesignResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        _mapper.Map(request, existing);

        if (request.Image != null)
        {
            // Delete the old image from Cloudinary
            if (!string.IsNullOrEmpty(existing.ImagePublicId))
            {
                await _fileService.DeleteImageAsync(existing.ImagePublicId, cancellationToken);
            }
            // Upload the new image to Cloudinary and get the URL
            var uploadResult = await _fileService.UploadImageAsync(request.Image, "Bottle_Design", cancellationToken);
            existing.ImageUrl = uploadResult.Url;
            existing.ImagePublicId = uploadResult.PublicId;
        }

        await _unitOfWork.bottleDesignRepository.UpdateAsync(existing, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<BottleDesignResponse>(existing);

        return Success(dto);
    }
    #endregion
}
