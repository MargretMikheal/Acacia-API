namespace Acacia.Data.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<BottleDesign> BottleDesigns { get; set; }
        public ICollection<PriceListItem> PriceListItems { get; set; }
        public ICollection<FinalProduct> FinalProducts { get; set; }
    }

}
