using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class NohaVmValidator : AbstractValidator<NohaVm>
    {
        public NohaVmValidator()
        {
            RuleFor(x => x.LetterNo)
                .NotEmpty()
                .WithMessage("লেটার নং আবশ্যক।")
                .MaximumLength(100)
                .WithMessage("লেটার নং ১০০ অক্ষরের মধ্যে হতে হবে।");

            RuleFor(x => x.NohaDate)
                .NotEmpty()
                .WithMessage("এনওএইচ তারিখ আবশ্যক।");


            RuleFor(x => x.DocumentFile)
               .NotNull()
               .When(x => x.NohaId == 0) // Apply this rule only when creating a new Noha (NohaId == 0)
               .WithMessage("এনওএইচএ ডকুমেন্ট আপলোড করতে হবে।")
               .Must(file => file != null && file.ContentLength > 0)
               .When(x => x.NohaId == 0) // Apply this check only when creating a new Noha
               .WithMessage("ফাইল খালি হতে পারবে না।")
               .Must(file => file.ContentType == "application/pdf")
               .When(x => x.NohaId == 0) // Apply this check only when creating a new Noha
               .WithMessage("শুধুমাত্র PDF ফাইল অনুমোদিত।");
        }
    }
}