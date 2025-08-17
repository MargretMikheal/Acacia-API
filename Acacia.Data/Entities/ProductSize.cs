namespace Acacia.Data.Entities;

public class ProductSize : BaseEntity
{
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    public decimal? SizeValue { get; set; }


    public ICollection<PriceListItem> PriceListItems { get; set; }
    public ICollection<FinalProduct> FinalProducts { get; set; }
}
