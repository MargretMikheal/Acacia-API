using Acacia.Data.Enums;

namespace Acacia.Data.Entities
{
    public class Oil : BaseEntity
    {
        public string Name { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public OilCategory Category { get; set; }
        public OilStyle Style { get; set; }

        public string CategoryAr { get; set; }
        public string CategoryEn { get; set; }

        public string StyleAr { get; set; }
        public string StyleEn { get; set; }

        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }

        public decimal? ScoreSummer { get; set; }
        public decimal? ScoreWinter { get; set; }
        public decimal? ScoreFall { get; set; }
        public decimal? ScoreSpring { get; set; }

        public decimal? ScoreFormal { get; set; }
        public decimal? ScoreCasual { get; set; }
        public decimal? ScoreNight { get; set; }
        public decimal? ScoreSport { get; set; }

        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }

        public ICollection<OilIngredient> OilIngredients { get; set; }
        public ICollection<FinalProduct> FinalProducts { get; set; }
    }

}
