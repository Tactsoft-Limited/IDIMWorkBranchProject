using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ReceivePaymentVm
    {
        public ReceivePaymentVm()
        {
            ConstructionFirmDropdown = new List<SelectListItem>();
        }

        public int ReceivePaymentId { get; set; }

        [Display(Name = "Project ID")]
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Construction Firm")]
        public int ConstructionFirmId { get; set; }

        [Display(Name = "Firm Name")]
        public string ConstructionFirmName { get; set; }

        [Required(ErrorMessage = "Letter Number is required.")]
        [MaxLength(150, ErrorMessage = "Letter Number cannot be more than 150 characters.")]
        [Display(Name = "Letter Number")]
        public string LetterNo { get; set; }

        [Required(ErrorMessage = "Bill Date is required.")]
        [Display(Name = "Bill Date")]
        [DataType(DataType.Date)]
        public DateTime BillDate { get; set; }

        [Display(Name = "Extra Time")]
        [DataType(DataType.Date)]
        public DateTime ExtraTime { get; set; }

        [MaxLength(200, ErrorMessage = "Bill Payment Sector cannot exceed 200 characters.")]
        [Display(Name = "Bill Payment Sector")]
        public string BillPaymentSector { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive number.")]
        [Display(Name = "Budget")]
        public decimal Budget { get; set; }

        [Range(0, 100, ErrorMessage = "Work Progress must be between 0 and 100.")]
        [Display(Name = "Actual Progress of Work (Physical) (%)")]
        public int WorkProgress { get; set; }

        [Range(0, 100, ErrorMessage = "Financial Progress must be between 0 and 100.")]
        [Display(Name = "Financial progress of work (%)")]
        public double FinancialProgress { get; set; }

        [Required(ErrorMessage = "Bill Number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Bill Number must be greater than 0.")]
        [Display(Name = "Bill Number")]
        public int BillNumber { get; set; }

        [Range(0, 100, ErrorMessage = "Bill Percentage must be between 0 and 100.")]
        [Display(Name = "Bill Percentage")]
        public int BillPercentage { get; set; }

        [Required(ErrorMessage = "Bill Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Bill Amount must be a positive number.")]
        [Display(Name = "Bill Amount")]
        public decimal BillAmount { get; set; }

        public string Remarks { get; set; }

        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }

    }
}