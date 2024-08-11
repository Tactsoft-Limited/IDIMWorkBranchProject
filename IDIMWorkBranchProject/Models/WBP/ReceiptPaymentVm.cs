using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ReceiptPaymentVm
    {
        public ReceiptPaymentVm()
        {
            FiscalYearDropdown = new List<SelectListItem>();
            QuarterDropdown = new List<SelectListItem>();
        }

        [Display(Name = "Id")]
        public int ReceiptPaymentId { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [Display(Name = "Project")]
        public string ProjectName { get; set; }

        [Display(Name = "Financial Year")]
        public int FiscalYearId { get; set; }
        [Display(Name = "Financial Year")]
        public string FiscalYearName { get; set; }

        [Display(Name = "Quarter")]
        public int QuarterId { get; set; }
        [Display(Name = "Quarter")]
        public string QuarterName { get; set; }

        [Display(Name = "Document Type")]
        public DocumentType DocumentType { get; set; }
        [Display(Name = "Source Type")]
        public SourceType SourceType { get; set; }

        [Display(Name = "Receipt Amount")]
        public double ReceiptAmount { get; set; }
        [Display(Name = "Receipt Date")]
        [DataType(DataType.Date)]
        public DateTime? ReceiptDate { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
        public IEnumerable<SelectListItem> QuarterDropdown { get; set; }
    }
}