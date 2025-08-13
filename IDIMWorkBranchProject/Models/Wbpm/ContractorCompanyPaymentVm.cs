using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ContractorCompanyPaymentVmValidator))]
    public class ContractorCompanyPaymentVm
    {

        public int ContractorCompanyPaymentId { get; set; }

        [DisplayName("প্রকল্প কাজ আইডি")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("লেটার নং")]
        public string LetterNo { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয়")]
        public decimal? EstimatedCost { get; set; }

        [DisplayName("আয়কর (%)")]
        public double EstimatedCostTaxPer { get; set; }

        [DisplayName("আয়কর পরিমান")]
        public decimal EstimatedCostTaxAmount { get; set; }

        [DisplayName("ভ্যাট (%)")]
        public double EstimatedCostVatPer { get; set; }

        [DisplayName("ভ্যাট পরিমান")]
        public decimal EstimatedCostVatAmount { get; set; }

        [DisplayName("জামানত (%)")]
        public double EstimatedCostCollateralPer { get; set; }

        [DisplayName("জামানত পরিমান")]
        public decimal EstimatedCostCollateralAmount { get; set; }

        [DisplayName("মোট কর্তনের পরিমান")]
        public decimal EstimatedCostDeductionAmount { get; set; }

        [DisplayName("নীট টাকার পরিমান")]
        public decimal NetEstimatedCostAmount { get; set; }

        [DisplayName("অগ্রগতি (%)")]
        public double ProgressPer { get; set; }

        [DisplayName("অগ্রগতি পরিমান")]
        public decimal ProgressAmount { get; set; }

        [DisplayName("আয়কর (%)")]
        public double ProgressTaxPer { get; set; }

        [DisplayName("আয়কর পরিমান")]
        public double ProgressTaxAmount { get; set; }

        [DisplayName("ভ্যাট (%)")]
        public double ProgressVatPer { get; set; }

        [DisplayName("ভ্যাট পরিমান")]
        public double ProgressVatAmount { get; set; }

        [DisplayName("জামানত (%)")]
        public double ProgressCollateralPer { get; set; }

        [DisplayName("জামানত পরিমান")]
        public double ProgressCollateralAmount { get; set; }

        [DisplayName("মোট কর্তনের পরিমান")]
        public decimal ProgressDeductionAmount { get; set; }

        [DisplayName("নীট অগ্রগতি পরিমান")]
        public decimal NetProgressAmount { get; set; }

        [DisplayName("পারফরম্যান্স সিকিউরিটি (%)")]
        public double PerformanceSecurityPer { get; set; }

        [DisplayName("পারফরম্যান্স সিকিউরিটি পরিমান")]
        public decimal PerformanceSecurityAmount { get; set; }

        [DisplayName("অগ্রগতি (%) অনুসারে ঠিকাদারের প্রাপ্য")]
        public double ContactorProgressPer { get; set; }

        [DisplayName("অগ্রগতি অনুসারে ঠিকাদারের প্রাপ্য পরিমান")]
        public decimal ContactorProgressAmount { get; set; }

        [DisplayName("বিল পেমেন্ট নম্বর")]
        public int BillPaymentNumber { get; set; }

        public int PreviouslyTotalPaidNo { get; set; }

        [DisplayName("ইতিপূর্বে ঠিকাদারকে নীট পরিশোধ")]
        public decimal PreviouslyPaidAmount { get; set; }

        [DisplayName("চলতি বিলে পরিশোধযোগ্য অর্থ")]
        public decimal PayableAmountOnCurrentBill { get; set; }

        [DisplayName("নীট পরিশোধযোগ্য অর্থ")]
        public decimal FinalPaymentAmount { get; set; }

        [DisplayName("নীট পরিশোধযোগ্য অর্থ কথায়")]
        public string FinalPaymentAmountInWord { get; set; }

        [DisplayName("বিবিধ ফান্ডে অবশিষ্ট জমা থাকবে")]
        public decimal WillBeDepositedInFund { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }

        [DisplayName("সংশ্লিষ্ট ফার্মের নামে বিজিবি বিবিধ ফান্ডে জমা")]
        public decimal? TotalDepositsInFund { get; set; }

        public decimal TotalWithdrawFromMinistry { get; set; }

        public double TotalWithdrawPercent { get; set; }

        public DateTime WorkStarted { get; set; }

        public DateTime WorkEnded { get; set; }


        [DisplayName("ঠিকাদারি প্রতিষ্ঠান")]
        public string ConstructionCompany { get; set; }

        [DisplayName("প্রধান সহকারী")]

        public int? HeadAssistantId { get; set; }

        [DisplayName("প্রধান সহকারী")]
        public string HeadAssistantName { get; set; }

        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public int? ConcernedEngineerId { get; set; }

        [DisplayName("ইঞ্জিনিয়ার আইসি")]
        public string ConcernedEngineerName { get; set; }

        [DisplayName("সেকশন আইসি")]
        public int? SectionICId { get; set; }

        [DisplayName("সেকশন আইসি")]
        public string SectionICTName { get; set; }

        [DisplayName("শাখা করনিক")]
        public int? BranchClerkId { get; set; }

        [DisplayName("শাখা করনিক")]
        public string BranchClerkName { get; set; }


        public IEnumerable<SelectListItem> HeadAssistantDropdown { get; set; }
        public IEnumerable<SelectListItem> ConcernedEngineerDropdown { get; set; }
        public IEnumerable<SelectListItem> SectionICTDropdown { get; set; }
        public IEnumerable<SelectListItem> BranchClerkDropdown { get; set; }
    }
}