using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ReceiptPaymentSearchVm
    {
        public ReceiptPaymentSearchVm()
        {
            FiscalYearDropdown = new List<SelectListItem>();
            QuarterDropdown = new List<SelectListItem>();
            ReceiptPayments = new List<ReceiptPaymentVm>();
        }

        [Display(Name = "Project")]
        public string ProjectName { get; set; }
        [Display(Name = "Fiscal Year")]
        public int FiscalYearId { get; set; }
        [Display(Name = "Quarter")]
        public int QuarterId { get; set; }
        [Display(Name = "Document Type")]
        public DocumentType? DocumentType { get; set; }
        [Display(Name = "Source Type")]
        public SourceType? SourceType { get; set; }

        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
        public IEnumerable<SelectListItem> QuarterDropdown { get; set; }

        public List<ReceiptPaymentVm> ReceiptPayments { get; set; }
        
    }

    public enum  DocumentType
    {
        DDP = 1,
        RDAP = 2
    }
}