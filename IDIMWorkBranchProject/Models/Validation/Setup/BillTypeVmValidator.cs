using FluentValidation;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Models.Validation.Setup
{
    public class BillTypeVmValidator : AbstractValidator<BillTypeVm>
    {
        public BillTypeVmValidator()
        {
            RuleFor(e => e.BillTypeName)
                .NotEmpty();
        }
    }
}