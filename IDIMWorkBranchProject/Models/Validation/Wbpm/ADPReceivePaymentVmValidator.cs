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
                .GreaterThan(0).WithMessage("নির্মাণ কাজ আবশ্যক।");

            // LetterNo: Required and should not be empty
            RuleFor(x => x.LetterNo)
                .NotEmpty().WithMessage("লেটার নম্বর আবশ্যক।");

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
                .InclusiveBetween(0, 100).WithMessage("বিল পরিশোধ (%) 0 থেকে 100 এর মধ্যে হতে হবে।");

            // BillPaidAmount: Required and should be greater than or equal to 0
            RuleFor(x => x.BillPaidAmount)
                .GreaterThanOrEqualTo(0).WithMessage("বিল পরিশোধের পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            // TaxPer: Required and should be between 0 and 100
            RuleFor(x => x.TaxPer)
                .InclusiveBetween(0, 100).WithMessage("ট্যাক্স (%) 0 থেকে 100 এর মধ্যে হতে হবে।");

            // TaxAmount: Required and should be greater than or equal to 0
            RuleFor(x => x.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("ট্যাক্স পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            // VatPer: Required and should be between 0 and 100
            RuleFor(x => x.VatPer)
                .InclusiveBetween(0, 100).WithMessage("ভ্যাট (%) 0 থেকে 100 এর মধ্যে হতে হবে।");

            // VatAmount: Required and should be greater than or equal to 0
            RuleFor(x => x.VatAmount)
                .GreaterThanOrEqualTo(0).WithMessage("ভ্যাট পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            // CollateralPer: Required and should be between 0 and 100
            RuleFor(x => x.CollateralPer)
                .InclusiveBetween(0, 100).WithMessage("জামানত (%) 0 থেকে 100 এর মধ্যে হতে হবে।");

            // CollateralAmount: Required and should be greater than or equal to 0
            RuleFor(x => x.CollateralAmount)
                .GreaterThanOrEqualTo(0).WithMessage("জামানত পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            // TotalDeductionAmount: Required and should be greater than or equal to 0
            RuleFor(x => x.TotalDeductionAmount)
                .GreaterThanOrEqualTo(0).WithMessage("মোট কাটা পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            // DepositInBGBFund: Required and should be greater than or equal to 0
            RuleFor(x => x.DepositInBGBFund)
                .GreaterThanOrEqualTo(0).WithMessage("বিবিধ ফান্ডে জমা শূন্য বা তার বেশি হতে হবে।");

            // IsDepositeBGBFund: No validation needed, since it's a boolean.

            // Remarks: Optional, but if present, should not exceed 250 characters
            RuleFor(x => x.Remarks)
                .MaximumLength(250).WithMessage("মন্তব্য সর্বাধিক 250 অক্ষরের মধ্যে হতে হবে।");
        }
    }
}
