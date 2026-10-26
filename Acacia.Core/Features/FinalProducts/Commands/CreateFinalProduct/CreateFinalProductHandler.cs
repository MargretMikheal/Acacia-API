using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using Acacia.Data.Entities.FinalProducts;
using Acacia.Data.Enums;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.FinalProducts.Commands.CreateFinalProduct;

public class CreateFinalProductHandler : ResponseHandler,
    IRequestHandler<CreateFinalProductCommand, Response<FinalProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _localizer;

    public CreateFinalProductHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<Response<FinalProductResponse>> Handle(CreateFinalProductCommand request, CancellationToken cancellationToken)
    {
        var oilExists = await _unitOfWork.oilRepository.ExistsAsync(request.OilId, cancellationToken);
        if (!oilExists)
        {
            return NotFound<FinalProductResponse>("Fragrance Oil not found.", new Dictionary<string, List<string>>
            {
                ["OilId"] = new List<string> { "Specified Fragrance Oil does not exist." }
            });
        }

        var productTypeExists = await _unitOfWork.productTypeRepository.ExistsAsync(request.ProductTypeId, cancellationToken);
        if (!productTypeExists)
        {
            return NotFound<FinalProductResponse>("Product Type not found.", new Dictionary<string, List<string>>
            {
                ["ProductTypeId"] = new List<string> { "Specified Product Type does not exist." }
            });
        }

        var productSizeExists = await _unitOfWork.productSizeRepository.ExistsAsync(request.ProductSizeId, cancellationToken);
        if (!productSizeExists)
        {
            return NotFound<FinalProductResponse>("Product Size not found.", new Dictionary<string, List<string>>
            {
                ["ProductSizeId"] = new List<string> { "Specified Product Size does not exist." }
            });
        }

        var bottleDesignExists = await _unitOfWork.bottleDesignRepository.ExistsAsync(request.BottleDesignId, cancellationToken);
        if (!bottleDesignExists)
        {
            return NotFound<FinalProductResponse>("Bottle Design not found.", new Dictionary<string, List<string>>
            {
                ["BottleDesignId"] = new List<string> { "Specified Bottle Design does not exist." }
            });
        }

        FinalProduct entity = (ProductTypeEnum)request.ProductTypeId switch
        {
            ProductTypeEnum.Perfume => new PerfumeFinalProduct
            {
                AlcoholPercentage = request.AlcoholPercentage ?? 20m
            },
            ProductTypeEnum.Mukhammaria => new MukhammariaFinalProduct(),
            ProductTypeEnum.BodySplash => new BodySplashFinalProduct(),
            ProductTypeEnum.Candle => new CandleFinalProduct(),
            ProductTypeEnum.BodyLotion => new BodyLotionFinalProduct(),
            ProductTypeEnum.Incense => new IncenseFinalProduct(),
            _ => new FinalProduct()
        };

        entity.OilId = request.OilId;
        entity.ProductTypeId = request.ProductTypeId;
        entity.ProductSizeId = request.ProductSizeId;
        entity.BottleDesignId = request.BottleDesignId;
        entity.BasePrice = request.BasePrice;
        entity.TotalPrice = request.TotalPrice;

        await _unitOfWork.finalProductRepository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var loaded = await _unitOfWork.finalProductRepository.GetByIdWithDetailsAsync(entity.Id, cancellationToken);
        var dto = _mapper.Map<FinalProductResponse>(loaded ?? entity);

        return Created(dto);
    }
}
