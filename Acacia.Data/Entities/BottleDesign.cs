namespace Acacia.Data.Entities
{
    public class BottleDesign : BaseEntity
    {
        public string Name { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<FinalProduct> FinalProducts { get; set; }
    }

}
