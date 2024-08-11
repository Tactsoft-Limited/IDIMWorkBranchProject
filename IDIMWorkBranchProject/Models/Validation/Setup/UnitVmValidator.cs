using FluentValidation;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Models.Validation.Setup
{
    public class UnitVmValidator : AbstractValidator<UnitVm>
    {
        public UnitVmValidator()
        {
            RuleFor(e => e.UnitName)
                .NotEmpty();
        }
    }
}