using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using Acacia.Data.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Oils.Queries.GetOilById
{
    public class GetOilByIdHandler : ResponseHandler,
        IRequestHandler<GetOilByIdQuery, Response<OilResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetOilByIdHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<OilResponse>> Handle(GetOilByIdQuery request, CancellationToken cancellationToken)
        {
            var oil = await _unitOfWork.oilRepository.GetByIdWithIncludesAsync(request.Id, cancellationToken);

            if (oil is null)
            {
                var error = new Dictionary<string, List<string>>
                {
                    { nameof(Oil), new List<string> { _localizer[SharedResourcesKeys.NotFound] } }
                };
                return NotFound<OilResponse>(_localizer[SharedResourcesKeys.NotFound], error);
            }

            var dto = _mapper.Map<OilResponse>(oil);
            return Success(dto);
        }
    }
}
