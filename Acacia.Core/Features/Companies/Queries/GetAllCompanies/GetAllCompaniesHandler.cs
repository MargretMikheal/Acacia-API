using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Interfaces.Services;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Companies.Queries.GetAllCompanies
{
    public class GetAllCompaniesHandler : ResponseHandler,
        IRequestHandler<GetAllCompaniesQuery, Response<List<CompanyResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IStorageService _storageService;

        public GetAllCompaniesHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer,
            IStorageService storageService
        ) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _storageService = storageService;
        }

        public async Task<Response<List<CompanyResponse>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
        {
            var companies = await _unitOfWork.companyRepository.GetAllAsync(cancellationToken);

            var dto = _mapper.Map<List<CompanyResponse>>(companies);
            foreach (var c in dto)
            {
                c.ImageUrl = _storageService.ResolvePresignedUrl(c.ImageUrl) ?? c.ImageUrl;
            }

            return Success(dto);
        }
    }
}
