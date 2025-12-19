using Acacia.Core.Bases;
using Acacia.Core.Interfaces.IReposetories;
using Acacia.Core.Resources;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Acacia.Core.Features.Oils.Queries.GetAllOils
{
    public class GetAllOilsHandler : ResponseHandler,
        IRequestHandler<GetAllOilsQuery, Response<List<OilResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetAllOilsHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
        ) : base(localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<List<OilResponse>>> Handle(GetAllOilsQuery request, CancellationToken cancellationToken)
        {
            var oils = await _unitOfWork.oilRepository.GetAllWithIncludesAsync(cancellationToken);

            if (oils is null)
                return Success(new List<OilResponse>());

            var dto = _mapper.Map<List<OilResponse>>(oils);
            return Success(dto);
        }
    }
}
