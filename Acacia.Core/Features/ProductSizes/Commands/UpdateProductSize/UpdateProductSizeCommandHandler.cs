using Acacia.Core.Bases;
using Acacia.Core.Features.ProductSizes.Commands.CreateProductSize;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.ProductSizes.Commands.UpdateProductSize
{
    public class UpdateProductSizeCommandHandler : ResponseHandler,
        IRequestHandler<UpdateProductSizeCommand, Response<ProductSizeResponse>>
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Ctor
        public UpdateProductSizeCommandHandler(
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
        public async Task<Response<ProductSizeResponse>> Handle(UpdateProductSizeCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.productSizeRepository.GetByIdAsync(request.Id);
            if (existing == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(ProductSize), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<ProductSizeResponse>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            var productType = await _unitOfWork.productTypeRepository.GetByIdAsync(request.ProductTypeId);
            if (productType == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(ProductType), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<ProductSizeResponse>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            var exists = await _unitOfWork.productSizeRepository.ExistsForProductAsync(request.ProductTypeId, request.Size, request.Id);
            if (exists)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(ProductSize), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
                };
                return UnprocessableEntity<ProductSizeResponse>(error);
            }

            _mapper.Map(request, existing);

            await _unitOfWork.productSizeRepository.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProductSizeResponse>(existing);

            return Success(dto);
        }
        #endregion
    }
}
