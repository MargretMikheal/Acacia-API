using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyHandler : ResponseHandler,
        IRequestHandler<UpdateCompanyCommand, Response<CompanyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICloudinaryService _fileService;

        public UpdateCompanyHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer,
            ICloudinaryService fileService) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _fileService = fileService;
        }

        public async Task<Response<CompanyResponse>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.companyRepository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Company), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<CompanyResponse>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            var duplicate = await _unitOfWork.companyRepository
                .AnyAsync(x => x.Name == request.Name && x.Id != request.Id, cancellationToken);

            if (duplicate)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Company), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
                };
                return UnprocessableEntity<CompanyResponse>(error);
            }

            entity.Name = request.Name;

            if (request.Image != null)
            {
                if (!string.IsNullOrEmpty(entity.ImagePublicId))
                {
                    await _fileService.DeleteImageAsync(entity.ImagePublicId, cancellationToken);
                }

                var uploadResult = await _fileService.UploadImageAsync(request.Image, "Companies", cancellationToken);
                entity.ImageUrl = uploadResult.Url;
                entity.ImagePublicId = uploadResult.PublicId;
            }

            await _unitOfWork.companyRepository.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<CompanyResponse>(entity);
            return Success(dto);
        }
    }
}
