namespace Acacia.Core.Features.ProductSizes.Commands.CreateProductSize
{
    public class ProductSizeResponse
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public decimal Size { get; set; }
    }
}
