using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.ProductSizes.Queries.GetAllProductSizes;

public class GetAllProductSizesHandler : ResponseHandler,
    IRequestHandler<GetAllProductSizesQuery, Response<List<ProductSizeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public GetAllProductSizesHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Response<List<ProductSizeResponse>>> Handle(GetAllProductSizesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.productSizeRepository.GetAllAsync(cancellationToken);
        var dto = _mapper.Map<List<ProductSizeResponse>>(entities);
        return Success(dto);
    }
}
