using FluentValidation;

namespace Acacia.Core.Features.ProductSizes.Commands.CreateProductSize
{
    public class CreateProductSizeValidator : AbstractValidator<CreateProductSizeCommand>
    {
        public CreateProductSizeValidator()
        {
            RuleFor(x => x.ProductTypeId)
                .GreaterThan(0).WithMessage("ProductTypeId must be greater than 0.");

            RuleFor(x => x.Size)
                .GreaterThan(0).WithMessage("SizeId must be greater than 0.");
        }
    }
}
