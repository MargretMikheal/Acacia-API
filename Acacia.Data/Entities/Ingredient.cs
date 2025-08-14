using Acacia.Data.Commons;

namespace Acacia.Data.Entities;

public class Ingredient : GeneralLocalizableEntity
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }

    public string ImageUrl { get; set; }

    public ICollection<OilIngredient> OilIngredients { get; set; }
}
