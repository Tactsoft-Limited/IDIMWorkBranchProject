using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class BillPaymentVm
    {
        public BillPaymentVm()
        {
            BillTypeDropdown = new List<SelectListItem>();
            FiscalYearDropdown = new List<SelectListItem>();
        }

        [Display(Name = "Bill Payment")]
        public int BillPaymentId { get; set; }

        [Display(Name = "Financial Year")]
        public int FiscalYearId { get; set; }
        [Display(Name = "Financial Year")]
        public string FiscalYearName { get; set; }

        [Display(Name = "Sub Project")]
        public int SubProjectId { get; set; }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }

        [Display(Name = "Bill Type")]
        public int BillTypeId { get; set; }
        [Display(Name = "Bill Type")]
        public string BillTypeName { get; set; }

        [Display(Name = "Source Type")]
        public SourceType SourceType { get; set; }
        [Display(Name = "Payment Type")]
        public PaymentType PaymentType { get; set; }

        [Display(Name = "Payment Amount")]
        public double PaymentAmount { get; set; }

        [Display(Name = "Payment Date")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
        public IEnumerable<SelectListItem> BillTypeDropdown { get; set; }
    }

    public enum SourceType
    {
        Revenue = 1,
        Capital = 2
    }

    public enum PaymentType
    {
        Cash = 1,
        Cheque = 2,
        PayOrder=3
    }
}