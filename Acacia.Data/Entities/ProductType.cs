using Acacia.Data.Commons;

namespace Acacia.Data.Entities
{
    public class ProductType : GeneralLocalizableEntity
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<BottleDesign> BottleDesigns { get; set; }
        public ICollection<PriceListItem> PriceListItems { get; set; }
        public ICollection<FinalProduct> FinalProducts { get; set; }
    }

}
