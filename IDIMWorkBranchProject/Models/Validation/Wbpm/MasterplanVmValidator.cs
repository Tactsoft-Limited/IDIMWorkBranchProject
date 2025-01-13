using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class MasterplanVmValidator : AbstractValidator<MasterplanVm>
    {
        public MasterplanVmValidator()
        {
            // MasterplanId is auto-generated, so no validation needed for it

            RuleFor(x => x.ADPProjectId)
                .NotEmpty().WithMessage("প্রকল্প আইডি অবশ্যই পূর্ণ করতে হবে।");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("শিরোনাম অবশ্যই পূর্ণ করতে হবে।")
                .Length(1, 200).WithMessage("শিরোনাম ১ থেকে ২০০ অক্ষরের মধ্যে হতে হবে।");

            //RuleFor(x => x.Document)
            //    .NotEmpty().WithMessage("ডকুমেন্ট নাম অবশ্যই পূর্ণ করতে হবে।")
            //    .Length(1, 255).WithMessage("ডকুমেন্ট নাম ১ থেকে ২৫৫ অক্ষরের মধ্যে হতে হবে।");

            //// Validate file upload
            //RuleFor(x => x.DocumentFile)
            //    .Must(file => file == null || (file.ContentLength > 0 && file.ContentType.StartsWith("application/")))
            //    .WithMessage("যথাযথ ডকুমেন্ট ফাইল (যেমন PDF, Word) আপলোড করুন।");
        }
    }
}