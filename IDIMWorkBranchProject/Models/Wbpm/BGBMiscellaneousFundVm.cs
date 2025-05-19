using System.ComponentModel;
using System;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Wbpm;

namespace IDIMWorkBranchProject.Models.Wbpm
{
    [Validator(typeof(BGBMiscellaneousFundVmValidator))]
    public class BGBMiscellaneousFundVm
    {

        public int FundId { get; set; }

        [DisplayName("প্রকল্প কাজ আইডি")]
        public int ProjectWorkId { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitle { get; set; }

        [DisplayName("নির্মাণ কাজের নাম")]
        public string ProjectWorkTitleB { get; set; }

        [DisplayName("এডিপি পেমেন্ট আইডি")]
        public int? ADPReceivePaymentId { get; set; }

        [DisplayName("লেটার নম্বর")]
        public string LetterNo { get; set; }

        [DisplayName("জমা তারিখ")]
        public DateTime DepositeDate { get; set; }

        [DisplayName("পে অর্ডার নম্বর")]
        public string PayOrderNo { get; set; }

        [DisplayName("পে অর্ডার তারিখ")]
        public DateTime PayOrderDate { get; set; }

        [DisplayName("ব্যাংক নাম")]
        public string BankName { get; set; }

        [DisplayName("শাখার নাম")]
        public string BrunchName { get; set; }

        [DisplayName("অ্যাকাউন্ট নাম")]
        public string AccountName { get; set; }

        [DisplayName("অ্যাকাউন্ট নম্বর")]
        public string AccountNumber { get; set; }

        [DisplayName("পরিমাণ")]
        public decimal Amount { get; set; }

        [DisplayName("মন্তব্য")]
        public string Remarks { get; set; }

    }
}
