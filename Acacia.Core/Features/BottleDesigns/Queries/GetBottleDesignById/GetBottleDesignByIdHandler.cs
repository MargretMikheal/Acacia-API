using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.BottleDesigns.Queries.GetBottleDesignById;

public class GetBottleDesignByIdHandler : ResponseHandler,
    IRequestHandler<GetBottleDesignByIdQuery, Response<BottleDesignResponse>>
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IStorageService _storageService;
    #endregion

    #region Ctor
    public GetBottleDesignByIdHandler(
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
    #endregion

    #region Methods
    public async Task<Response<BottleDesignResponse>> Handle(GetBottleDesignByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.bottleDesignRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var error = new Dictionary<string, List<string>>
                {
                    { nameof(BottleDesign), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };

            return NotFound<BottleDesignResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        var dto = _mapper.Map<BottleDesignResponse>(entity);
        dto.ImageUrl = _storageService.ResolvePresignedUrl(entity.ImagePublicId ?? entity.ImageUrl) ?? dto.ImageUrl;

        return Success(dto);
    }
    #endregion
}
