using System.ComponentModel;
using System;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(ADPReceivePaymentVmValidator))]
    public class ADPReceivePaymentVm
    {

        public int ADPReceivePaymentId { get; set; }

        [DisplayName("নির্মাণ কাজ")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম (English)")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitleB { get; set; }

        [Display(Name = "ফার্মের নাম (English)")]
        public string ConstructionFirm { get; set; }

        [DisplayName("লেটার নম্বর (English)")]
        public string LetterNo { get; set; }

        [DisplayName("বিল নম্বর (English)")]
        public int BillNumber { get; set; }

        [DisplayName("বিল তারিখ")]
        public DateTime BillDate { get; set; }

        [DisplayName("অতিরিক্ত সময়")]
        public DateTime? ExtraTime { get; set; }

        [DisplayName("বিল পেমেন্ট সেক্টর")]
        public string BillPaymentSector { get; set; }

        [DisplayName("প্রাক্কলিত ব্যয় (English)")]
        public decimal? EstimatedCost { get; set; }

        [DisplayName("কাজের বাস্তব অগ্রগতি (ভৌত %) (English)")]
        public double ActualWorkProgressPer { get; set; }

        [DisplayName("আর্থিক অগ্রগতি (%) (English)")]
        public double? FinancialProgressPer { get; set; }

        [DisplayName("এ পর্যন্ত বিল পরিশোধ (%)(English)")]
        public double? BillPaidPerTillDate { get; set; }

        [DisplayName("এ পর্যন্ত বিল পরিশোধ (English)")]
        public decimal? BillPaidAmountTillDate { get; set; }

        [DisplayName("বিল পরিশোধ (%) (English)")]
        public double BillPaidPer { get; set; }

        [DisplayName("বিল পরিশোধের পরিমাণ (English)")]
        public decimal BillPaidAmount { get; set; }

        [DisplayName("বিল পরিশোধের পরিমাণ (কথায়)")]
        public string BillPaidAmountInWord { get; set; }

        [DisplayName("মোট পরিশোধ (%) (English)")]
        public double? TotalReceivePer { get; set; }

        [DisplayName("মোট পরিশোধ অর্থ (English)")]
        public decimal? TotalReceiveAmount { get; set; }

        [DisplayName("মোট পরিশোধ অর্থ (কথায়)")]
        public string TotalReceiveAmountInWord { get; set; }

        [DisplayName("বিবিধ ফান্ডে জমা করা হয়েছে?")]
        public bool IsDepositeBGBFund { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }
        
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

        [DisplayName("অফিসার")]
        public int? OfficerId { get; set; }

        [DisplayName("অফিসার")]
        public string officerName { get; set; }


        public IEnumerable<SelectListItem> HeadAssistantDropdown { get; set; }
        public IEnumerable<SelectListItem> ConcernedEngineerDropdown { get; set; }
        public IEnumerable<SelectListItem> SectionICTDropdown { get; set; }
        public IEnumerable<SelectListItem> BranchClerkDropdown { get; set; }
        public IEnumerable<SelectListItem> OfficersDropdown { get; set; }
    }
}