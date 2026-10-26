using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.FinalProducts.Queries.GetFinalProductById;

public class GetFinalProductByIdHandler : ResponseHandler,
    IRequestHandler<GetFinalProductByIdQuery, Response<FinalProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IStorageService _storageService;

    public GetFinalProductByIdHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<SharedResources> localizer,
        IStorageService storageService) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizer = localizer;
        _storageService = storageService;
    }

    public async Task<Response<FinalProductResponse>> Handle(GetFinalProductByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.finalProductRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            var errors = new Dictionary<string, List<string>>
            {
                ["FinalProduct"] = new List<string> { _localizer[SharedResourcesKeys.NotFound] }
            };
            return NotFound<FinalProductResponse>(_localizer[SharedResourcesKeys.NotFound], errors);
        }

        var dto = _mapper.Map<FinalProductResponse>(entity);
        if (!string.IsNullOrWhiteSpace(dto.BottleDesignImageUrl))
        {
            dto.BottleDesignImageUrl = _storageService.ResolvePresignedUrl(dto.BottleDesignImageUrl) ?? dto.BottleDesignImageUrl;
        }
        return Success(dto);
    }
}
