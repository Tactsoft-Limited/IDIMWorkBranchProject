using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class PerformanceSecurityVmValidator : AbstractValidator<PerformanceSecurityVm>
    {
        public PerformanceSecurityVmValidator()
        {
            RuleFor(x => x.SubmissionDate)
                .NotEmpty()
                .WithMessage("জমার তারিখ আবশ্যক।");

            RuleFor(x => x.ExpiryDate)
                .NotEmpty()
                .WithMessage("ব্যাংক গ্যারান্টির শেষ তারিখ আবশ্যক।");
            
        }
    }
}