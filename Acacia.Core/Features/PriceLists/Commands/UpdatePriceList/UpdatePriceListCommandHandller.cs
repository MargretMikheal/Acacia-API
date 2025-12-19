using Acacia.Core.Bases;
using Acacia.Core.Features.ProductSizes;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.PriceLists.Commands.UpdatePriceList;

public class UpdatePriceListCommandHandller : ResponseHandler,
        IRequestHandler<UpdatePriceListCommand, Response<PriceListResponse>>
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion

    #region Ctor
    public UpdatePriceListCommandHandller(
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
    public async Task<Response<PriceListResponse>> Handle(UpdatePriceListCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.priceListRepository.GetByIdAsync(request.Id);
        if (existing == null)
        {
            var error = new Dictionary<string, List<string>>
                {
                    { nameof(PriceList), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
            return NotFound<PriceListResponse>(_localizer[SharedResourcesKeys.NotFound], error);
        }

        var exists = await _unitOfWork.priceListRepository.ExistsByNameAsync(request.Name);
        if (exists)
        {
            var error = new Dictionary<string, List<string>>
                {
                    { nameof(PriceList), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
                };
            return UnprocessableEntity<PriceListResponse>(error);
        }

        _mapper.Map(request, existing);

        await _unitOfWork.priceListRepository.UpdateAsync(existing, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<PriceListResponse>(existing);

        return Success(dto);
    }
    #endregion
}
