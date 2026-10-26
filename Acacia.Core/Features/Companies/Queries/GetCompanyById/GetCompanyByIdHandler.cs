using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdHandler : ResponseHandler,
        IRequestHandler<GetCompanyByIdQuery, Response<CompanyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IStorageService _storageService;

        public GetCompanyByIdHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer,
            IStorageService storageService)
            : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _storageService = storageService;
        }

        public async Task<Response<CompanyResponse>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.companyRepository.GetByIdAsync(request.Id, cancellationToken);
            if (company == null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Company), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<CompanyResponse>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            var dto = _mapper.Map<CompanyResponse>(company);
            dto.ImageUrl = _storageService.ResolvePresignedUrl(company.ImagePublicId ?? company.ImageUrl) ?? dto.ImageUrl;
            return Success(dto);
        }
    }
}
