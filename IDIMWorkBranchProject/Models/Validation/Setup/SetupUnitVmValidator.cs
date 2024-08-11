using FluentValidation;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Models.Validation.Setup
{
    public class SetupUnitVmValidator : AbstractValidator<UnitVm>
    {
        public SetupUnitVmValidator()
        {
            RuleFor(e => e.UnitName)
                .NotEmpty();
        }
    }
}