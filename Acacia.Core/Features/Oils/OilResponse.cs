namespace Acacia.Core.Features.Oils;

public class OilResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }

    public int CompanyId { get; set; }
    public int PriceListId { get; set; }

    public string Category { get; set; }
    public string Style { get; set; }

    public List<int> IngredientIds { get; set; }

    public OilSeasonScoreResponse SeasonScore { get; set; }
    public OilOccasionScoreResponse OccasionScore { get; set; }
}

public class OilSeasonScoreResponse
{
    public decimal? Summer { get; set; }
    public decimal? Winter { get; set; }
    public decimal? Fall { get; set; }
    public decimal? Spring { get; set; }
}

public class OilOccasionScoreResponse
{
    public decimal? Formal { get; set; }
    public decimal? Casual { get; set; }
    public decimal? Night { get; set; }
    public decimal? Sport { get; set; }
}
