using Acacia.Core.Bases;
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
                { nameof(BottleDesign), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
            };
            return UnprocessableEntity<BottleDesignResponse>(error);
        }

        var entity = _mapper.Map<BottleDesign>(request);

        // Upload image to Cloudinary and get the URL
        entity.ImageUrl = await _fileService.UploadImageAsync(request.Image, "bottle_designs", cancellationToken);

        var created = await _unitOfWork.bottleDesignReposetory.AddAsync(entity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<BottleDesignResponse>(created);

        return Created(dto);
    }
    #endregion
}
