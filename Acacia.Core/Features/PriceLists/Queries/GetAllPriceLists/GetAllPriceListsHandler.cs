using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.PriceLists.Queries.GetAllPriceLists;

public class GetAllPriceListsHandler : ResponseHandler,
    IRequestHandler<GetAllPriceListsQuery, Response<List<PriceListResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public GetAllPriceListsHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Response<List<PriceListResponse>>> Handle(GetAllPriceListsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.priceListRepository.GetAllAsync(cancellationToken);
        var dto = _mapper.Map<List<PriceListResponse>>(entities);
        return Success(dto);
    }
}
