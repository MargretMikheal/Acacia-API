using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById
{
    public class GetProductTypeByIdHandler : ResponseHandler,
        IRequestHandler<GetProductTypeByIdQuery, Response<ProductTypeResponse>>
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Ctor
        public GetProductTypeByIdHandler(
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
        public async Task<Response<ProductTypeResponse>> Handle(GetProductTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var productType = await _unitOfWork.productTypeRepository.GetByIdAsync(request.Id);

            if (productType == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(ProductType), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<ProductTypeResponse>(_localizer[SharedResourcesKeys.NotFound]);
            }

            var dto = _mapper.Map<ProductTypeResponse>(productType);

            return Success(dto);
        }
        #endregion
    }
}
