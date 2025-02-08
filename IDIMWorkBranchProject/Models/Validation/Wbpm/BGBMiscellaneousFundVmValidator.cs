using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class BGBMiscellaneousFundVmValidator : AbstractValidator<BGBMiscellaneousFundVm>
    {
        public BGBMiscellaneousFundVmValidator()
        {
            // ProjectWorkId: Required and should be greater than 0
            RuleFor(x => x.ProjectWorkId)
                .GreaterThan(0).WithMessage("প্রকল্প কাজ আইডি আবশ্যক এবং 0 এর বেশি হতে হবে।");

            // ADPReceivePaymentId: Required and should be greater than 0
            RuleFor(x => x.ADPReceivePaymentId)
                .GreaterThan(0).WithMessage("এডিপি পেমেন্ট আইডি আবশ্যক এবং 0 এর বেশি হতে হবে।");

            // LetterNo: Required and should not be empty
            RuleFor(x => x.LetterNo)
                .NotEmpty().WithMessage("লেটার নম্বর আবশ্যক।");

            // DepositeDate: Required and should be a valid date
            RuleFor(x => x.DepositeDate)
                .NotEmpty().WithMessage("জমা তারিখ আবশ্যক।");

            // PayOrderNo: Required and should not be empty
            RuleFor(x => x.PayOrderNo)
                .NotEmpty().WithMessage("পে অর্ডার নম্বর আবশ্যক।");

            // PayOrderDate: Required and should be a valid date
            RuleFor(x => x.PayOrderDate)
                .NotEmpty().WithMessage("পে অর্ডার তারিখ আবশ্যক।")
                .GreaterThan(x => x.DepositeDate).WithMessage("পে অর্ডার তারিখ জমা তারিখের পরে হতে হবে।");

            // BankName: Required and should not be empty
            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("ব্যাংক নাম আবশ্যক।")
                .MaximumLength(150).WithMessage("ব্যাংক নাম সর্বাধিক 150 অক্ষরের মধ্যে হতে হবে।");

            // BrunchName: Required and should not be empty
            RuleFor(x => x.BrunchName)
                .NotEmpty().WithMessage("শাখার নাম আবশ্যক।")
                .MaximumLength(150).WithMessage("শাখার নাম সর্বাধিক 150 অক্ষরের মধ্যে হতে হবে।");

            // AccountName: Required and should not be empty
            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("অ্যাকাউন্ট নাম আবশ্যক।")
                .MaximumLength(150).WithMessage("অ্যাকাউন্ট নাম সর্বাধিক 150 অক্ষরের মধ্যে হতে হবে।");

            // AccountNumber: Required and should not be empty
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("অ্যাকাউন্ট নম্বর আবশ্যক।")
                .MaximumLength(150).WithMessage("অ্যাকাউন্ট নম্বর সর্বাধিক 150 অক্ষরের মধ্যে হতে হবে।");

            // Amount: Required and should be greater than or equal to 0
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0).WithMessage("পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            // Remarks: Optional but should not exceed 250 characters
            RuleFor(x => x.Remarks)
                .MaximumLength(250).WithMessage("মন্তব্য সর্বাধিক 250 অক্ষরের মধ্যে হতে হবে।");
        }
    }
}