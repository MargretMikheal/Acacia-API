using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.FinalProducts.Queries.GetAllFinalProducts;

public class GetAllFinalProductsHandler : ResponseHandler,
    IRequestHandler<GetAllFinalProductsQuery, Response<List<FinalProductResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IStorageService _storageService;

    public GetAllFinalProductsHandler(
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

    public async Task<Response<List<FinalProductResponse>>> Handle(GetAllFinalProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.finalProductRepository.GetWithDetailsAsync(cancellationToken);
        var dto = _mapper.Map<List<FinalProductResponse>>(entities);
        foreach (var p in dto)
        {
            if (!string.IsNullOrWhiteSpace(p.BottleDesignImageUrl))
            {
                p.BottleDesignImageUrl = _storageService.ResolvePresignedUrl(p.BottleDesignImageUrl) ?? p.BottleDesignImageUrl;
            }
        }
        return Success(dto);
    }
}
