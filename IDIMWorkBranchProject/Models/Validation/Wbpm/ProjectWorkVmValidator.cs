using FluentValidation;

using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ProjectWorkVmValidator : AbstractValidator<ProjectWorkVm>
    {
        public ProjectWorkVmValidator()
        {


            // ProjectWorkTitle - required field
            RuleFor(x => x.ProjectWorkTitle)
                .NotEmpty().WithMessage("নির্মাণ কাজের নাম অবশ্যই পূর্ণ করতে হবে।")
                .Length(1, 500).WithMessage("নির্মাণ কাজের নাম ১ থেকে ৫০০ অক্ষরের মধ্যে হতে হবে।");

            // EstimatedCost - optional, but must be a valid decimal if provided
            RuleFor(x => x.EstimatedCost)
                .GreaterThanOrEqualTo(0).WithMessage("প্রাক্কলিত ব্যয় নেতিবাচক হতে পারে না।");

            // Remarks - optional field, max length validation
            RuleFor(x => x.Remarks)
                .MaximumLength(300).WithMessage("মন্তব্য ৩০০ অক্ষরের মধ্যে হতে হবে।");
        }
    }
}