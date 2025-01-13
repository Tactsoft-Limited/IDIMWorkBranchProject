using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ProjectWorkVmValidator : AbstractValidator<ProjectWorkVm>
    {
        public ProjectWorkVmValidator()
        {
            // ADPProjectId - required field
            RuleFor(x => x.ADPProjectId)
                .NotEmpty().WithMessage("প্রকল্প আইডি অবশ্যই পূর্ণ করতে হবে।");

            // ConstructionCompanyId - required field
            RuleFor(x => x.ConstructionCompanyId)
                .NotEmpty().WithMessage("নির্মাণ কোম্পানি আইডি অবশ্যই পূর্ণ করতে হবে।");

            // ProjectWorkTitle - required field
            RuleFor(x => x.ProjectWorkTitle)
                .NotEmpty().WithMessage("নির্মাণ কাজের নাম অবশ্যই পূর্ণ করতে হবে।")
                .Length(1, 500).WithMessage("নির্মাণ কাজের নাম ১ থেকে ৫০০ অক্ষরের মধ্যে হতে হবে।");

            // EstimatedCost - optional, but must be a valid decimal if provided
            RuleFor(x => x.EstimatedCost)
                .GreaterThanOrEqualTo(0).WithMessage("প্রাক্কলিত ব্যয় নেতিবাচক হতে পারে না।");

            // NOADate - required field and must be a valid date
            RuleFor(x => x.NOADate)
                .NotEmpty().WithMessage("এনওএইচ তারিখ অবশ্যই পূর্ণ করতে হবে।")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("এনওএইচ তারিখ ভবিষ্যতের হতে পারে না।");

            // AgreementDate - required field and must be a valid date
            RuleFor(x => x.AgreementDate)
                .NotEmpty().WithMessage("চুক্তির তারিখ অবশ্যই পূর্ণ করতে হবে।")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("চুক্তির তারিখ ভবিষ্যতের হতে পারে না।");

            // WorkOrderDate - required field and must be a valid date
            RuleFor(x => x.WorkOrderDate)
                .NotEmpty().WithMessage("কার্যাদেশের তারিখ অবশ্যই পূর্ণ করতে হবে।")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("কার্যাদেশের তারিখ ভবিষ্যতের হতে পারে না।");

            // WorkOrderTime - required field
            RuleFor(x => x.WorkOrderTime)
                .NotEmpty().WithMessage("কার্যাদেশের সময় অবশ্যই পূর্ণ করতে হবে.");

            // WorkStartDate - required field
            RuleFor(x => x.WorkStartDate)
                .NotEmpty().WithMessage("কাজ শুরুর তারিখ অবশ্যই পূর্ণ করতে হবে.")
                .GreaterThanOrEqualTo(x => x.WorkOrderDate).WithMessage("কাজ শুরুর তারিখ কার্যাদেশের তারিখের পর হতে হবে।");

            // WorkEndDate - required field
            RuleFor(x => x.WorkEndDate)
                .NotEmpty().WithMessage("কাজ শেষের তারিখ অবশ্যই পূর্ণ করতে হবে.")
                .GreaterThanOrEqualTo(x => x.WorkStartDate).WithMessage("কাজ শেষের তারিখ কাজ শুরুর তারিখের পর হতে হবে।");

            // OnFieldStartDate - optional
            RuleFor(x => x.OnFieldStartDate)
                .LessThanOrEqualTo(x => x.WorkStartDate).When(x => x.OnFieldStartDate.HasValue)
                .WithMessage("সরেজমিনে শুরুর তারিখ কাজ শুরুর তারিখের পর হতে পারে না।");

            // OnFieldEndDate - optional
            RuleFor(x => x.OnFieldEndDate)
                .LessThanOrEqualTo(x => x.WorkEndDate).When(x => x.OnFieldEndDate.HasValue)
                .WithMessage("সরেজমিনে শেষের তারিখ কাজ শেষের তারিখের পর হতে পারে না।");

            // HandOverDate - required field
            RuleFor(x => x.HandOverDate)
                .NotEmpty().WithMessage("হস্তান্তর তারিখ অবশ্যই পূর্ণ করতে হবে.")
                .GreaterThanOrEqualTo(x => x.WorkEndDate).WithMessage("হস্তান্তর তারিখ কাজ শেষের তারিখের পর হতে হবে।");

            // BankGuaranteeEndDate - required field
            RuleFor(x => x.BankGuaranteeEndDate)
                .NotEmpty().WithMessage("ব্যাংক গ্যারান্টির শেষ তারিখ অবশ্যই পূর্ণ করতে হবে.")
                .GreaterThanOrEqualTo(x => x.WorkEndDate).WithMessage("ব্যাংক গ্যারান্টির শেষ তারিখ কাজ শেষের তারিখের পর হতে হবে।");

            // ConstructionProgress - required field and must be between 0 and 100
            RuleFor(x => x.ConstructionProgress)
                .InclusiveBetween(0, 100).WithMessage("নির্মাণ অগ্রগতি ০ থেকে ১০০ এর মধ্যে হতে হবে।");

            //// BankGuaranteeDocument - required
            //RuleFor(x => x.BankGuaranteeDocument)
            //    .NotEmpty().WithMessage("ব্যাংক গ্যারান্টি ডকুমেন্ট নাম অবশ্যই পূর্ণ করতে হবে।")
            //    .Length(1, 255).WithMessage("ব্যাংক গ্যারান্টি ডকুমেন্ট নাম ১ থেকে ২৫৫ অক্ষরের মধ্যে হতে হবে।");

            //// NOADocument - required
            //RuleFor(x => x.NOADocument)
            //    .NotEmpty().WithMessage("এনওএইচ ডকুমেন্ট নাম অবশ্যই পূর্ণ করতে হবে।")
            //    .Length(1, 255).WithMessage("এনওএইচ ডকুমেন্ট নাম ১ থেকে ২৫৫ অক্ষরের মধ্যে হতে হবে।");

            //// AgreementDocument - required
            //RuleFor(x => x.AgreementDocument)
            //    .NotEmpty().WithMessage("চুক্তি ডকুমেন্ট নাম অবশ্যই পূর্ণ করতে হবে।")
            //    .Length(1, 255).WithMessage("চুক্তি ডকুমেন্ট নাম ১ থেকে ২৫৫ অক্ষরের মধ্যে হতে হবে।");

            //// WorkOrderDocument - required
            //RuleFor(x => x.WorkOrderDocument)
            //    .NotEmpty().WithMessage("কার্যাদেশ ডকুমেন্ট নাম অবশ্যই পূর্ণ করতে হবে।")
            //    .Length(1, 255).WithMessage("কার্যাদেশ ডকুমেন্ট নাম ১ থেকে ২৫৫ অক্ষরের মধ্যে হতে হবে।");

            //// File upload validation for all document files
            //RuleFor(x => x.BankGuaranteeDocumentFile)
            //    .Must(file => file == null || (file.ContentLength > 0 && file.ContentType.StartsWith("application/")))
            //    .WithMessage("ব্যাংক গ্যারান্টি ডকুমেন্ট ফাইল (যেমন PDF, Word) আপলোড করুন।");

            //RuleFor(x => x.NOADocumentFile)
            //    .Must(file => file == null || (file.ContentLength > 0 && file.ContentType.StartsWith("application/")))
            //    .WithMessage("এনওএইচ ডকুমেন্ট ফাইল (যেমন PDF, Word) আপলোড করুন।");

            //RuleFor(x => x.AgreementDocumentFile)
            //    .Must(file => file == null || (file.ContentLength > 0 && file.ContentType.StartsWith("application/")))
            //    .WithMessage("চুক্তি ডকুমেন্ট ফাইল (যেমন PDF, Word) আপলোড করুন।");

            //RuleFor(x => x.WorkOrderDocumentFile)
            //    .Must(file => file == null || (file.ContentLength > 0 && file.ContentType.StartsWith("application/")))
            //    .WithMessage("কার্যাদেশ ডকুমেন্ট ফাইল (যেমন PDF, Word) আপলোড করুন।");

            // WorkStatus - required field
            RuleFor(x => x.WorkStatus)
                .NotEmpty().WithMessage("কাজের অবস্থা অবশ্যই পূর্ণ করতে হবে।");

            // Remarks - optional field, max length validation
            RuleFor(x => x.Remarks)
                .MaximumLength(300).WithMessage("মন্তব্য ৩০০ অক্ষরের মধ্যে হতে হবে।");
        }
    }
}