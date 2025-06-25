using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ProjectDirectorVmValidator : AbstractValidator<ProjectDirectorVm>
    {
        public ProjectDirectorVmValidator()
        {
            RuleFor(x => x.ProjectDirectorName)
                .NotEmpty().WithMessage("প্রকল্প পরিচালক নামটি অবশ্যই প্রদান করতে হবে।")
                .MaximumLength(150).WithMessage("প্রকল্প পরিচালক নামটি 150 অক্ষরের বেশি হতে পারবে না।");

            RuleFor(x => x.Designation)
                .NotEmpty().WithMessage("পদবি অবশ্যই প্রদান করতে হবে।")
                .MaximumLength(150).WithMessage("পদবি 150 অক্ষরের বেশি হতে পারবে না।");

            RuleFor(x => x.JoiningDate)
                .NotEmpty().WithMessage("যোগদান তারিখ অবশ্যই প্রদান করতে হবে।");

            RuleFor(x => x.TransferDate)
                .GreaterThan(x => x.JoiningDate).When(x => x.TransferDate.HasValue)
                .WithMessage("বদলীর তারিখ যোগদান তারিখের পরে হতে হবে।");
        }
    }
}