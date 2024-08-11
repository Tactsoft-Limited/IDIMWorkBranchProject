using FluentValidation;
using IDIMWorkBranchProject.Models.Setup;

namespace IDIMWorkBranchProject.Models.Validation.Setup
{
    public class QuarterVmValidator : AbstractValidator<QuarterVm>
    {
        public QuarterVmValidator()
        {
            RuleFor(m => m.QuarterName)
                .NotEmpty();

            RuleFor(m => m.MonthFrom)
                .NotEmpty()
                .Must(m => m >= 1 && m <= 12);

            RuleFor(m => m.MonthTo)
                .NotEmpty()
                .Must(m => m >= 1 && m <= 12);

            RuleFor(m => m.YearFrom)
                .NotEmpty();
        }
    }
}