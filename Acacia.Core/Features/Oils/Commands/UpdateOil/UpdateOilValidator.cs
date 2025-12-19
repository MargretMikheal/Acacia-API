using FluentValidation;

namespace Acacia.Core.Features.Oils.Commands.UpdateOil;

public class UpdateOilValidator : AbstractValidator<UpdateOilCommand>
{
    public UpdateOilValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.CompanyId).GreaterThan(0);
        RuleFor(x => x.PriceListId).GreaterThan(0);
        RuleFor(x => x.DescriptionAr).NotEmpty();
        RuleFor(x => x.DescriptionEn).NotEmpty();
    }
}
