using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class ContractorCompanyPaymentVmValidator : AbstractValidator<ContractorCompanyPaymentVm>
    {
        public ContractorCompanyPaymentVmValidator()
        {
            RuleFor(x => x.LetterNo)
               .NotEmpty().WithMessage("লেটার নম্বর আবশ্যক।");

            RuleFor(x => x.EstimatedCostTaxPer)
                .InclusiveBetween(0, 100).WithMessage("ট্যাক্স শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.EstimatedCostTaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("ট্যাক্স পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.EstimatedCostVatPer)
                .InclusiveBetween(0, 100).WithMessage("ভ্যাট শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.EstimatedCostVatAmount)
                .GreaterThanOrEqualTo(0).WithMessage("ভ্যাট পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.EstimatedCostCollateralPer)
                .InclusiveBetween(0, 100).WithMessage("জামানত শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.EstimatedCostCollateralAmount)
                .GreaterThanOrEqualTo(0).WithMessage("জামানত পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.EstimatedCostDeductionAmount)
                .GreaterThanOrEqualTo(0).WithMessage("মোট কর্তনের পরিমান শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.NetEstimatedCostAmount)
                .GreaterThan(0).WithMessage("নেট পরিমাণ 0 এর বেশি হতে হবে।");

            RuleFor(x => x.ProgressPer)
                .InclusiveBetween(0, 100).WithMessage("অগ্রগতি শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.ProgressAmount)
                .GreaterThanOrEqualTo(0).WithMessage("অগ্রগতি পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.ProgressTaxPer)
                .InclusiveBetween(0, 100).WithMessage("ট্যাক্স শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.ProgressTaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("ট্যাক্স পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.ProgressVatPer)
                .InclusiveBetween(0, 100).WithMessage("ভ্যাট শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.ProgressVatAmount)
                .GreaterThanOrEqualTo(0).WithMessage("ভ্যাট পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.ProgressCollateralPer)
                .InclusiveBetween(0, 100).WithMessage("জামানত শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.ProgressCollateralAmount)
                .GreaterThanOrEqualTo(0).WithMessage("জামানত পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.ProgressDeductionAmount)
                .GreaterThanOrEqualTo(0).WithMessage("মোট কর্তনের পরিমান শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.NetProgressAmount)
                .GreaterThanOrEqualTo(0).WithMessage("নেট পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.PerformanceSecurityPer)
                .InclusiveBetween(0, 100).WithMessage("পারফরম্যান্স সিকিউরিটি শতাংশ 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.PerformanceSecurityAmount)
                .GreaterThanOrEqualTo(0).WithMessage("পারফরম্যান্স সিকিউরিটি পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.ContactorProgressPer)
                .InclusiveBetween(0, 100).WithMessage("অগ্রগতি (%) অনুসারে ঠিকাদারের প্রাপ্য 0 থেকে 100 এর মধ্যে হতে হবে।");

            RuleFor(x => x.ContactorProgressAmount)
                .GreaterThanOrEqualTo(0).WithMessage("অগ্রগতি অনুসারে ঠিকাদারের প্রাপ্য পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.BillPaymentNumber)
                .GreaterThan(0).WithMessage("বিল পেমেন্ট নম্বর শূন্যের বেশি হতে হবে।");

            RuleFor(x => x.PreviouslyPaidAmount)
                .GreaterThanOrEqualTo(0).WithMessage("ইতিপূর্বে ঠিকাদারকে নীট পরিশোধ পরিমাণ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.PayableAmountOnCurrentBill)
                .NotEmpty().WithMessage("চলতি বিলের পরিশোধযোগ্য অর্থ আবশ্যক।")
                .GreaterThanOrEqualTo(0).WithMessage("চলতি বিলের পরিশোধযোগ্য অর্থ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.FinalPaymentAmount)
                .Must(x => x >= 0)
                .WithMessage("নীট পরিশোধযোগ্য অর্থ শূন্য বা তার বেশি হতে হবে।");

            RuleFor(x => x.FinalPaymentAmount)
                .Must((model, finalPaymentAmount) => finalPaymentAmount <= model.TotalDepositsInFund)
                .WithMessage("দুঃখিত বিবিধ ফান্ডে পর্যাপ্ত পরিমান অর্থ জমা নাই! ");

            RuleFor(x => x.WillBeDepositedInFund)
                .GreaterThanOrEqualTo(0)
                .WithMessage("দুঃখিত বিবিধ ফান্ডে পর্যাপ্ত পরিমান অর্থ জমা নাই! ");

            RuleFor(x => x.Remarks)
                .MaximumLength(150).WithMessage("মন্তব্য 150 অক্ষরের মধ্যে থাকতে হবে।");
        }
    }
}