using FluentValidation;

namespace Acacia.Core.Features.ProductSizes.Commands.UpdateProductSize
{
    public class UpdateProductSizeValidator : AbstractValidator<UpdateProductSizeCommand>
    {
        public UpdateProductSizeValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid ProductSize Id");

            RuleFor(x => x.ProductTypeId)
                .GreaterThan(0).WithMessage("ProductType Id must be greater than 0");

            RuleFor(x => x.Size)
                .GreaterThan(0).WithMessage("Size must be greater than 0");
        }
    }
}
