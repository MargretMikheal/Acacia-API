using Acacia.Core.Bases;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.ProductSizes.Commands.CreateProductSize
{
    public class ProductSizeCommandHandler : ResponseHandler,
        IRequestHandler<CreateProductSizeCommand, Response<ProductSizeResponse>>
    {
        #region Fields
        private readonly IProductSizeService _productSizeService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Ctor
        public ProductSizeCommandHandler(
            IProductSizeService productSizeService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _productSizeService = productSizeService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Methods
        public async Task<Response<ProductSizeResponse>> Handle(CreateProductSizeCommand request, CancellationToken cancellationToken)
        {
            //var productType = await _productTypeService.GetByIdAsync(request.ProductTypeId);
            //if (productType == null)
            //{
            //    return NotFound<ProductSizeResponse>(_localizer[SharedResourcesKeys.NotFound]);
            //}

            var exists = await _productSizeService.ExistsForProductAsync(request.ProductTypeId, request.Size);
            if (exists)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(ProductSize), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
                };

                return UnprocessableEntity<ProductSizeResponse>(error);
            }

            var entity = _mapper.Map<ProductSize>(request);

            var created = await _productSizeService.AddAsync(entity);

            var dto = _mapper.Map<ProductSizeResponse>(created);

            return Created(dto);
        }
        #endregion
    }
}
