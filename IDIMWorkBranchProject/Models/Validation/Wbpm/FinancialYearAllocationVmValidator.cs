using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class FinancialYearAllocationVmValidator : AbstractValidator<FinancialYearAllocationVm>
    {
        public FinancialYearAllocationVmValidator()
        {
            // FinancialYearAllocationId is auto-generated, so no validation needed

            RuleFor(x => x.ADPProjectId)
                .NotEmpty().WithMessage("প্রকল্প আইডি অবশ্যই পূর্ণ করতে হবে।");

            RuleFor(x => x.FiscalYearId)
                .NotEmpty().WithMessage("আর্থবছর আইডি অবশ্যই পূর্ণ করতে হবে।");

            RuleFor(x => x.TotalAllocation)
                .GreaterThanOrEqualTo(0).WithMessage("মোট বরাদ্দ ঋণাত্মক হতে পারে না।");

            RuleFor(x => x.ADPAllocation)
                .GreaterThanOrEqualTo(0).WithMessage("এডিপি বরাদ্দ ঋণাত্মক হতে পারে না।");

            RuleFor(x => x.RADPAllocation)
                .GreaterThanOrEqualTo(0).WithMessage("আরএডিপি বরাদ্দ ঋণাত্মক হতে পারে না।");

        }
    }
}