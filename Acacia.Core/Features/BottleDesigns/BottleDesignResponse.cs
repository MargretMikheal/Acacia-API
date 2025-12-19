namespace Acacia.Core.Features.BottleDesigns;

public class BottleDesignResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string ImagePublicId { get; set; }
}
