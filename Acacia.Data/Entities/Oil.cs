using Acacia.Data.Commons;
using Acacia.Data.Enums;

namespace Acacia.Data.Entities;

public class Oil : GeneralLocalizableEntity
{
    public string Name { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public OilCategoryEnum Category { get; set; }
    public OilStyleEnum Style { get; set; }

    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }

    public int PriceListId { get; set; }
    public PriceList PriceList { get; set; }

    public ICollection<OilIngredient> OilIngredients { get; set; }
    public ICollection<FinalProduct> FinalProducts { get; set; }

    public OilSeasonScore SeasonScore { get; set; }
    public OilOccasionScore OccasionScore { get; set; }
}

