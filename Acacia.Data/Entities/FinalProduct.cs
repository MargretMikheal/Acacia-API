namespace Acacia.Data.Entities;

public class FinalProduct : BaseEntity
{
    public int OilId { get; set; }
    public Oil Oil { get; set; }

    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    public int ProductSizeId { get; set; }
    public ProductSize ProductSize { get; set; }

    public int BottleDesignId { get; set; }
    public BottleDesign BottleDesign { get; set; }

    public decimal BasePrice { get; set; }
    public decimal TotalPrice { get; set; }
}
