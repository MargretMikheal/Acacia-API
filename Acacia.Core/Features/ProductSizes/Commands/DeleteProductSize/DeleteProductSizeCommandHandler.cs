using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.ProductSizes.Commands.DeleteProductSize
{
    public class DeleteProductSizeCommandHandler : ResponseHandler,
        IRequestHandler<DeleteProductSizeCommand, Response<string>>
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Ctor
        public DeleteProductSizeCommandHandler(
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }
        #endregion

        #region Methods
        public async Task<Response<string>> Handle(DeleteProductSizeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.productSizeRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                var error = new Dictionary<string, List<string>>
                 {
                     { nameof(ProductSize), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                 };
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            await _unitOfWork.productSizeRepository.DeleteAsync(entity.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Success<string>(_localizer[SharedResourcesKeys.Deleted]);

        }
        #endregion
    }
}
