using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.BottleDesigns.Commands.DeleteBottleDesign;

public class DeleteBottleDesignHandller : ResponseHandler,
    IRequestHandler<DeleteBottleDesignCommand, Response<string>>
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly ICloudinaryService _fileService;
    #endregion

    #region Ctor
    public DeleteBottleDesignHandller(
        IUnitOfWork unitOfWork,
        IStringLocalizer<SharedResources> localizer,
        ICloudinaryService fileService
    ) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
        _fileService = fileService;
    }
    #endregion

    #region Methods
    public async Task<Response<string>> Handle(DeleteBottleDesignCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.bottleDesignReposetory.GetByIdAsync(request.Id);
        if (existing == null)
        {
            var error = new Dictionary<string, List<string>>
                 {
                     { nameof(BottleDesign), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                 };
            return NotFound<string>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        // Delete image from Cloudinary
        if (!string.IsNullOrEmpty(existing.ImagePublicId))
        {
            await _fileService.DeleteImageAsync(existing.ImagePublicId, cancellationToken);
        }

        await _unitOfWork.bottleDesignReposetory.DeleteAsync(existing.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Success<string>(_localizer[SharedResourcesKeys.Deleted]);
    }
    #endregion
}
