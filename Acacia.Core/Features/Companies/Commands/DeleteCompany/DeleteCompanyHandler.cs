using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyHandler : ResponseHandler,
        IRequestHandler<DeleteCompanyCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _fileService;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public DeleteCompanyHandler(
            IUnitOfWork unitOfWork,
            ICloudinaryService fileService,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.companyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Company), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<string>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            if (!string.IsNullOrEmpty(entity.ImagePublicId))
            {
                await _fileService.DeleteImageAsync(entity.ImagePublicId, cancellationToken);
            }

            await _unitOfWork.companyRepository.DeleteAsync(entity.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Deleted<string>();
        }
    }
}
