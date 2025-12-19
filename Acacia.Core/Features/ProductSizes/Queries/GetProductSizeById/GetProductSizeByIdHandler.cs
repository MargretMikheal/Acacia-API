using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.ProductSizes.Queries.GetProductSizeById
{
    public class GetProductSizeByIdHandler : ResponseHandler,
        IRequestHandler<GetProductSizeByIdQuery, Response<ProductSizeResponse>>
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Ctor
        public GetProductSizeByIdHandler(
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
        public async Task<Response<ProductSizeResponse>> Handle(GetProductSizeByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.productSizeRepository.GetByIdAsync(request.Id);

            if (entity == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(ProductSize), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };

                return NotFound<ProductSizeResponse>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            var dto = _mapper.Map<ProductSizeResponse>(entity);

            return Success(dto);
        }
        #endregion
    }
}
