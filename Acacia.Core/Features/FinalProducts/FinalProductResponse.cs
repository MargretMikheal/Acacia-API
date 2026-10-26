namespace Acacia.Core.Features.FinalProducts;

public class FinalProductResponse
{
    public int Id { get; set; }
    public int OilId { get; set; }
    public string OilName { get; set; } = string.Empty;
    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; } = string.Empty;
    public int ProductSizeId { get; set; }
    public decimal ProductSizeValue { get; set; }
    public int BottleDesignId { get; set; }
    public string BottleDesignName { get; set; } = string.Empty;
    public string? BottleDesignImageUrl { get; set; }
    public decimal BasePrice { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal? AlcoholPercentage { get; set; }
    public string ProductTypeDiscriminator { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
}
