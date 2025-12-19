using FluentValidation;

namespace Acacia.Core.Features.Oils.Commands.DeleteOil;

public class DeleteOilValidator : AbstractValidator<DeleteOilCommand>
{
    public DeleteOilValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}
