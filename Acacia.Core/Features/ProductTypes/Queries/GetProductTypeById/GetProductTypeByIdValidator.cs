using FluentValidation;

namespace Acacia.Core.Features.ProductTypes.Queries.GetProductTypeById
{
    public class GetProductTypeByIdValidator : AbstractValidator<GetProductTypeByIdQuery>
    {
        public GetProductTypeByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ProductType Id must be greater than 0.");
        }
    }
}
