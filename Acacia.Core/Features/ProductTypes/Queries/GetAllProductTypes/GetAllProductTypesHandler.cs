using Acacia.Core.Bases;
using Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.ProductTypes.Queries.GetAllProductTypes;

public class GetAllProductTypesHandler : ResponseHandler,
    IRequestHandler<GetAllProductTypesQuery, Response<List<ProductTypeResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public GetAllProductTypesHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Response<List<ProductTypeResponse>>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.productTypeRepository.GetAllAsync(cancellationToken);
        var dto = _mapper.Map<List<ProductTypeResponse>>(entities);
        return Success(dto);
    }
}
