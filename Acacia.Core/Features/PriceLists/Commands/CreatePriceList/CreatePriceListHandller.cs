using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.PriceLists.Commands.CreatePriceList;

public class CreatePriceListHandller : ResponseHandler,
        IRequestHandler<CreatePriceListCommand, Response<PriceListResponse>>
{
    #region Fields
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;
    #endregion

    #region Ctor
    public CreatePriceListHandller(
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
    public async Task<Response<PriceListResponse>> Handle(CreatePriceListCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.priceListRepository.ExistsByNameAsync(request.Name, cancellationToken);
        if (exists)
        {
            var error = new Dictionary<string, List<string>>
            {
                { nameof(PriceList), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
            };
            return UnprocessableEntity<PriceListResponse>(error);
        }

        var entity = _mapper.Map<PriceList>(request);

        var created = await _unitOfWork.priceListRepository.AddAsync(entity, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<PriceListResponse>(created);

        return Created(dto);
    }
    #endregion
}
