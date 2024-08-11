using FluentValidation;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Models.Validation.Setup
{
    public class ConstructionFrimVmValidator : AbstractValidator<ConstructionFirmVm>
    {
        public ConstructionFrimVmValidator()
        {
            RuleFor(e => e.ConstructionFirmName)
                .NotEmpty();
        }
    }
}