using Acacia.Core.Bases;
using Acacia.Core.Features.Oils.Commands.CreateOil;
using MediatR;

namespace Acacia.Core.Features.Oils.Commands.UpdateOil;

public class UpdateOilCommand : IRequest<Response<OilResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public int CompanyId { get; set; }
    public int PriceListId { get; set; }

    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }

    public int Category { get; set; }
    public int Style { get; set; }

    public List<int> IngredientIds { get; set; } = new();

    public OilSeasonScoreRequest? SeasonScore { get; set; }
    public OilOccasionScoreRequest? OccasionScore { get; set; }
}
