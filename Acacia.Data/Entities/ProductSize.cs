namespace Acacia.Data.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public decimal SizeValue { get; set; }
        public string SizeUnit { get; set; }

        public ICollection<PriceListItem> PriceListItems { get; set; }
        public ICollection<FinalProduct> FinalProducts { get; set; }
    }

}
