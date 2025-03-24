using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ADPReceivePaymentVmValidator : AbstractValidator<ADPReceivePaymentVm>
    {
        public ADPReceivePaymentVmValidator()
        {
            // ProjectWorkId: Required and should be greater than 0
            RuleFor(x => x.ProjectWorkId)
                .GreaterThan(0).WithMessage("নির্মাণ কাজ অবশ্যই পূরণ করতে হবে।");

            // LetterNo: Required and should not be empty
            RuleFor(x => x.LetterNo)
                .NotEmpty().WithMessage("লেটার নম্বর অবশ্যই পূরণ করতে হবে।");

            // BillNumber: Required and should be greater than 0
            RuleFor(x => x.BillNumber)
                .GreaterThan(0).WithMessage("বিল নম্বর আবশ্যক এবং 0 এর বেশি হতে হবে।");

            // BillDate: Required and should be a valid date
            RuleFor(x => x.BillDate)
                .NotEmpty().WithMessage("বিল তারিখ আবশ্যক।");

            // BillPaymentSector: Required and should not be empty
            RuleFor(x => x.BillPaymentSector)
                .NotEmpty().WithMessage("বিল পেমেন্ট সেক্টর আবশ্যক।");

            // EstimatedCost: Optional, but if present, should be greater than or equal to 0
            RuleFor(x => x.EstimatedCost)
                .GreaterThanOrEqualTo(0).When(x => x.EstimatedCost.HasValue).WithMessage("প্রাক্কলিত ব্যয় শূন্য বা তার বেশি হতে হবে।");

            // ActualWorkProgressPer: Required and should be between 0 and 100
            RuleFor(x => x.ActualWorkProgressPer)
                .InclusiveBetween(0, 100).WithMessage("কাজের বাস্তব অগ্রগতি (ভৌত %) 0 থেকে 100 এর মধ্যে হতে হবে।");

            // FinancialProgressPer: Required and should be between 0 and 100
            RuleFor(x => x.FinancialProgressPer)
                .InclusiveBetween(0, 100).WithMessage("আর্থিক অগ্রগতি (%) 0 থেকে 100 এর মধ্যে হতে হবে।");

            // BillPaidPer: Required and should be between 0 and 100
            RuleFor(x => x.BillPaidPer)
                .NotEmpty().WithMessage("বিল পরিশোধ (%) অবশ্যই পূরণ করতে হবে।")
                .InclusiveBetween(0, 100).WithMessage("বিল পরিশোধ (%) 0 থেকে 100 এর মধ্যে হতে হবে।");

            // BillPaidAmount: Required and should be greater than or equal to 0
            RuleFor(x => x.BillPaidAmount)
                .GreaterThanOrEqualTo(0).WithMessage("বিল পরিশোধের পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            // IsDepositeBGBFund: No validation needed, since it's a boolean.

            // Remarks: Optional, but if present, should not exceed 250 characters
            RuleFor(x => x.Remarks)
                .MaximumLength(250).WithMessage("মন্তব্য সর্বাধিক 250 অক্ষরের মধ্যে হতে হবে।");
        }
    }
}
