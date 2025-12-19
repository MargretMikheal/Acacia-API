using Acacia.Core.Bases;
using Acacia.Core.Features.ProductSizes;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.PriceLists.Queries.GetPriceListById;

public class GetPriceListByIdHandler : ResponseHandler,
    IRequestHandler<GetPriceListByIdQuery, Response<PriceListResponse>>

{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion

    #region Ctor
    public GetPriceListByIdHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<SharedResources> localizer
    ) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizer = localizer;
    }
    #endregion

    #region Methods
    public async Task<Response<PriceListResponse>> Handle(GetPriceListByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.priceListRepository.GetByIdAsync(request.Id);

        if (entity == null)
        {
            var error = new Dictionary<string, List<string>>
                {
                    { nameof(ProductSize), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };

            return NotFound<PriceListResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        var dto = _mapper.Map<PriceListResponse>(entity);

        return Success(dto);
    }
    #endregion
}
