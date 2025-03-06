using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ADPProjectVmValidator : AbstractValidator<ADPProjectVm>
    {
        public ADPProjectVmValidator()
        {
            // ProjectTitle: Required and cannot be empty
            RuleFor(x => x.ProjectTitle)
                .NotEmpty().WithMessage("প্রকল্পের শিরোনাম আবশ্যক।")
                .MaximumLength(500).WithMessage("প্রকল্পের শিরোনাম সর্বাধিক 500 অক্ষরের মধ্যে হতে হবে।");

            // EstimatedExpenses: Required and should be greater than 0
            RuleFor(x => x.EstimatedExpenses)
                .GreaterThan(0).WithMessage("প্রাক্কলিত ব্যয় শূন্যের বেশি হতে হবে।");

            RuleFor(x => x.StartingDate)
               .NotEmpty().WithMessage("শুরু করার তারিখ আবশ্যক।");

            RuleFor(x => x.EndingDate)
                .NotEmpty().WithMessage("শেষ করার তারিখ আবশ্যক।");

            // StartingDate: Required and should be a valid future date
            //RuleFor(x => x.StartingDate)
            //    .NotEmpty().WithMessage("শুরু করার তারিখ আবশ্যক।")
            //    .LessThan(x => x.EndingDate).WithMessage("শুরু করার তারিখ শেষ করার তারিখের আগে হতে হবে।")
            //    .GreaterThan(DateTime.Now).WithMessage("শুরু করার তারিখ বর্তমান সময়ের পরে হতে হবে।");

            // EndingDate: Required and should be a valid future date
            //RuleFor(x => x.EndingDate)
            //    .NotEmpty().WithMessage("শেষ করার তারিখ আবশ্যক।")
            //    .GreaterThan(x => x.StartingDate).WithMessage("শেষ করার তারিখ শুরু করার তারিখের পরে হতে হবে।");

            // NoOfWork: Required and should be greater than 0
            RuleFor(x => x.NoOfWork)
                .GreaterThan(0).WithMessage("কাজের সংখ্যা শূন্যের বেশি হতে হবে।");

            // FinancialProgress: Should be between 0 and 100
            RuleFor(x => x.FinancialProgress)
                .InclusiveBetween(0, 100).WithMessage("আর্থিক অগ্রগতি 0 থেকে 100 এর মধ্যে হতে হবে।");

            // PhysicalProgress: Should be between 0 and 100
            RuleFor(x => x.PhysicalProgress)
                .InclusiveBetween(0, 100).WithMessage("ভৌত অগ্রগতি 0 থেকে 100 এর মধ্যে হতে হবে।");

            // Remarks: Optional, but if present, should not exceed 250 characters
            RuleFor(x => x.Remarks)
                .MaximumLength(250).WithMessage("মন্তব্য সর্বাধিক 250 অক্ষরের মধ্যে হতে হবে।");
        }
    }
}
