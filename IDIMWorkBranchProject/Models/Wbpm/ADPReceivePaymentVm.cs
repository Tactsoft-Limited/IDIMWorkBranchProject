using System.ComponentModel;
using System;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Wbpm
{
	[Validator(typeof(ADPReceivePaymentVmValidator))]
    public class ADPReceivePaymentVm
    {

        public int ADPReceivePaymentId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitle { get; set; }

		[Display(Name = "ফার্মের নাম")]
		public string ConstructionFirm { get; set; }

		[DisplayName("লেটার নম্বর")]
        public string LetterNo { get; set; }

        [DisplayName("বিল নম্বর")]
        public int BillNumber { get; set; }

        [DisplayName("বিল তারিখ")]
        public DateTime BillDate { get; set; }

        [DisplayName("অতিরিক্ত সময়")]
        public DateTime? ExtraTime { get; set; }

        [DisplayName("বিল পেমেন্ট সেক্টর")]
        public string BillPaymentSector { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal? EstimatedCost { get; set; }

        [DisplayName("কাজের বাস্তব অগ্রগতি (ভৌত %)")]
        public double ActualWorkProgressPer { get; set; }

        [DisplayName("আর্থিক অগ্রগতি (%)")]
        public double FinancialProgressPer { get; set; }

		[DisplayName("এ পর্যন্ত বিল পরিশোধ")]
		public decimal? BillPaidTillDate { get; set; }

		[DisplayName("বিল পরিশোধ (%)")]
        public double BillPaidPer { get; set; }

        [DisplayName("বিল পরিশোধের পরিমাণ")]
        public decimal BillPaidAmount { get; set; }

		[DisplayName("বিল পরিশোধের পরিমাণ (কথায়)")]
		public string BillPaidAmountInWord { get; set; }

		[DisplayName("ট্যাক্স (%)")]
        public double TaxPer { get; set; }

        [DisplayName("ট্যাক্স পরিমাণ")]
        public decimal TaxAmount { get; set; }

        [DisplayName("ভ্যাট (%)")]
        public double VatPer { get; set; }

        [DisplayName("ভ্যাট পরিমাণ")]
        public decimal VatAmount { get; set; }

        [DisplayName("জামানত (%)")]
        public double CollateralPer { get; set; }

        [DisplayName("জামানত পরিমাণ")]
        public decimal CollateralAmount { get; set; }

        [DisplayName("মোট কর্তনের পরিমাণ")]
        public decimal TotalDeductionAmount { get; set; }

        [DisplayName("বিবিধ ফান্ডে জমা")]
        public decimal DepositInBGBFund { get; set; }

        [DisplayName("বিবিধ ফান্ডে জমা করা হয়েছে?")]
        public bool IsDepositeBGBFund { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }


        [DisplayName("সিজিএ হতে উত্তোলিত অর্থ")]
        public decimal TotalWithdrawAmount { get; set; }

		[DisplayName("সিজিএ হতে উত্তোলিত অর্থ (%)")]
		public double TotalWithdrawPer { get; set; }

	}
}