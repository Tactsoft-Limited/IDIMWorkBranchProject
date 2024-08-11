using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class BillPaymentSearchVm
    {
        public BillPaymentSearchVm()
        {
            FiscalYearDropdown = new List<SelectListItem>();
            BillTypeDropdown = new List<SelectListItem>();
            BillPayments = new List<BillPaymentVm>();
        }

        [Display(Name = "Fiscal Year")]
        public int? FiscalYearId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        [Display(Name = "Bill Type")]
        public int? BillTypeId { get; set; }
        [Display(Name = "Source Type")]
        public SourceType? SourceType { get; set; }
        [Display(Name = "Payment Type")]
        public PaymentType? PaymentType { get; set; }

        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
        public IEnumerable<SelectListItem> BillTypeDropdown { get; set; }

        public List<BillPaymentVm> BillPayments { get; set; }

    }
}