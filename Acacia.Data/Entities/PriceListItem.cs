namespace Acacia.Data.Entities
{
    public class PriceListItem
    {
        public int Id { get; set; }
        public int PriceListId { get; set; }
        public PriceList PriceList { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public int ProductSizeId { get; set; }
        public ProductSize ProductSize { get; set; }

        public decimal Price { get; set; }
    }

}
