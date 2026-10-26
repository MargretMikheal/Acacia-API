using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.BottleDesigns.Queries.GetAllBottleDesigns;

public class GetAllBottleDesignsHandler : ResponseHandler,
    IRequestHandler<GetAllBottleDesignsQuery, Response<List<BottleDesignResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IStorageService _storageService;

    public GetAllBottleDesignsHandler(
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

    public async Task<Response<List<BottleDesignResponse>>> Handle(GetAllBottleDesignsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.bottleDesignRepository.GetAllAsync(cancellationToken);
        var dto = _mapper.Map<List<BottleDesignResponse>>(entities);
        foreach (var b in dto)
        {
            b.ImageUrl = _storageService.ResolvePresignedUrl(b.ImagePublicId ?? b.ImageUrl) ?? b.ImageUrl;
        }
        return Success(dto);
    }
}
