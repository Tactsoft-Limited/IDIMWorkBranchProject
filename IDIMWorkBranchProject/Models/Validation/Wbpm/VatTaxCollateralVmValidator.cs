using FluentValidation;
using IDIMWorkBranchProject.Models.Wbpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDIMWorkBranchProject.Models.Validation.Wbpm
{
    public class VatTaxCollateralVmValidator : AbstractValidator<VatTaxCollateralVm>
    {
        public VatTaxCollateralVmValidator() 
        {
            RuleFor(x => x.BillSubmissionNo)
                .NotEmpty().WithMessage("বিল জমা নম্বর দেয়া আবশ্যক");
            RuleFor(x => x.BillSubmissionDate)
                .NotEmpty().WithMessage("বিল জমার তারিখ দেয়া আবশ্যক");
            RuleFor(x => x.LastBillAmount)
                .NotEmpty().WithMessage("সর্বশেষ বিলের পরিমাণ দেয়া আবশ্যক");
            RuleFor(x => x.AllocatedAmountTillNow)
                .NotEmpty().WithMessage("এ পর্যন্ত বরাদ্দকৃত পরিমাণ দেয়া আবশ্যক");
            RuleFor(x => x.AllocatedAmountLetterNo)
                .NotEmpty().WithMessage("বরাদ্দকৃত পরিমাণের চিঠি নম্বর দেয়া আবশ্যক");
            RuleFor(x => x.ReducedAllocatedAmountTillNow)
                .NotEmpty().WithMessage("এ পর্যন্ত হ্রাসকৃত বরাদ্দের পরিমাণ দেয়া আবশ্যক");
            RuleFor(x => x.ReducedAllocatedAmountLetterNo)
                .NotEmpty().WithMessage("হ্রাসকৃত বরাদ্দের চিঠি নম্বর দেয়া আবশ্যক");
            RuleFor(x => x.NetTotalAmount)
                .NotEmpty().WithMessage("নেট মোট পরিমাণ দেয়া আবশ্যক");
            RuleFor(x => x.LastBillTotalBalance)
                .NotEmpty().WithMessage("সর্বশেষ বিলের মোট ব্যালেন্স দেয়া আবশ্যক");
            RuleFor(x => x.CurrentBillTotalBalance)
                .NotEmpty().WithMessage("বর্তমান বিলের মোট ব্যালেন্স দেয়া আবশ্যক");
            RuleFor(x => x.RelatedWorkBillAmount)
                .NotEmpty().WithMessage("সম্পর্কিত কাজের বিলের পরিমাণ দেয়া আবশ্যক");
            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("মোট পরিমাণ দেয়া আবশ্যক");
            RuleFor(x => x.CodeNo)
                .NotEmpty().WithMessage("কোড নম্বর দেয়া আবশ্যক");
            RuleFor(x => x.EconomicCode)
                .NotEmpty().WithMessage("অর্থনৈতিক কোড দেয়া আবশ্যক");
            RuleFor(x => x.VoucherNo)
                .NotEmpty().WithMessage("ভাউচার নম্বর দেয়া আবশ্যক");

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


        }
    }
}