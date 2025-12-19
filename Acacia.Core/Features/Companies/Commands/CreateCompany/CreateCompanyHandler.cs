using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyHandler : ResponseHandler,
        IRequestHandler<CreateCompanyCommand, Response<CompanyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ICloudinaryService _fileService;

        public CreateCompanyHandler(
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

        public async Task<Response<CompanyResponse>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var exists = await _unitOfWork.companyRepository
                .AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (exists)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Company), new List<string> { _localizer[SharedResourcesKeys.DuplicateEntry] } }
                };
                return UnprocessableEntity<CompanyResponse>(error);
            }

            var entity = _mapper.Map<Company>(request);

            var uploadResult = await _fileService.UploadImageAsync(request.Image, "Companies", cancellationToken);
            entity.ImageUrl = uploadResult.Url;
            entity.ImagePublicId = uploadResult.PublicId;

            var created = await _unitOfWork.companyRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<CompanyResponse>(created);

            return Created(dto);
        }
    }
}
