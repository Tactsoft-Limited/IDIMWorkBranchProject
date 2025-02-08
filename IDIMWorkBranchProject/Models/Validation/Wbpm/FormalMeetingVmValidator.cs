using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class FormalMeetingVmValidator : AbstractValidator<FormalMeetingVm>
    {
        public FormalMeetingVmValidator()
        {
            // FormalMeetingId is auto-generated, no validation needed

            RuleFor(x => x.ADPProjectId)
                .NotEmpty().WithMessage("প্রকল্প আইডি অবশ্যই পূর্ণ করতে হবে।");

            RuleFor(x => x.MeetingTitle)
                .NotEmpty().WithMessage("মিটিং শিরোনাম অবশ্যই পূর্ণ করতে হবে।")
                .Length(1, 150).WithMessage("মিটিং শিরোনাম ১ থেকে ১৫০ অক্ষরের মধ্যে হতে হবে।");

            RuleFor(x => x.NumberOfMeeting)
                .GreaterThan(0).WithMessage("মিটিং সংখ্যা অবশ্যই ১ বা তার বেশি হতে হবে।");

            RuleFor(x => x.MeetingDate)
                .NotEmpty().WithMessage("মিটিং তারিখ অবশ্যই পূর্ণ করতে হবে।")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("মিটিং তারিখ ভবিষ্যতের হতে পারে না।");

            //RuleFor(x => x.MeetingDocument)
            //    .Length(1, 150).WithMessage("মিটিং ডকুমেন্ট নাম ১ থেকে ১৫০ অক্ষরের মধ্যে হতে হবে।");

            // File validation for MeetingDocumentFile
            //RuleFor(x => x.MeetingDocumentFile)
            //    .Must(file => file == null || (file.ContentLength > 0 && file.ContentType.StartsWith("application/")))
            //    .WithMessage("যথাযথ মিটিং ডকুমেন্ট ফাইল আপলোড করুন।");

        }
    }
}