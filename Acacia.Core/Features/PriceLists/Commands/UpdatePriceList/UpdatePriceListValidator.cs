using FluentValidation;

namespace Acacia.Core.Features.PriceLists.Commands.UpdatePriceList;

public class UpdatePriceListValidator : AbstractValidator<UpdatePriceListCommand>
{
    public UpdatePriceListValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required.")
            .GreaterThan(0).WithMessage("Invalid ProductSize Id");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
