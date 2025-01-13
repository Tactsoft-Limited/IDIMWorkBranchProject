using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class FiscalYearExpenseVmValidator : AbstractValidator<FiscalYearExpenseVm>
    {
        public FiscalYearExpenseVmValidator()
        {
            // FiscalYearExpenseId is auto-generated, so no validation needed for it

            RuleFor(x => x.ADPProjectId)
                .NotEmpty().WithMessage("প্রকল্প আইডি অবশ্যই পূর্ণ করতে হবে।");

            RuleFor(x => x.FiscalYearId)
                .NotEmpty().WithMessage("আর্থবছর আইডি অবশ্যই পূর্ণ করতে হবে।");

            RuleFor(x => x.TotalExpense)
                .GreaterThanOrEqualTo(0).WithMessage("মোট খরচ ঋণাত্মক হতে পারে না।");
        }
    }
}