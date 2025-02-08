using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class VatTaxVm
    {
        public VatTaxVm()
        {
            ProjectDropdown = new List<SelectListItem>();
            ReceivePaymentDropdown = new List<SelectListItem>();
        }
        public int VatTaxId { get; set; }

        [Required(ErrorMessage = "Project ID is required.")]
        [Display(Name = "Project ID")]
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Receive Payment ID is required.")]
        [Display(Name = "Receive Payment ID")]
        public int ReceivePaymentId { get; set; }

        [DisplayName("Letter No")]
        public string LetterNumber { get; set; }

        public decimal ReceiveAmount { get; set; }

        [Required(ErrorMessage = "Tax Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Tax Percentage must be between 0 and 100.")]
        [Display(Name = "Tax Percentage")]
        public double TaxPer { get; set; }

        [Required(ErrorMessage = "Tax Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Tax Amount must be a positive number.")]
        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }

        [Required(ErrorMessage = "VAT Percentage is required.")]
        [Range(0, 100, ErrorMessage = "VAT Percentage must be between 0 and 100.")]
        [Display(Name = "VAT Percentage")]
        public double VatPer { get; set; }

        [Required(ErrorMessage = "VAT Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "VAT Amount must be a positive number.")]
        [Display(Name = "VAT Amount")]
        public decimal VatAmount { get; set; }

        [Required(ErrorMessage = "Collateral Percentage is required.")]
        [Range(0, 100, ErrorMessage = "Collateral Percentage must be between 0 and 100.")]
        [Display(Name = "Collateral Percentage")]
        public double CollateralPer { get; set; }

        [Required(ErrorMessage = "Collateral Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Collateral Amount must be a positive number.")]
        [Display(Name = "Collateral Amount")]
        public decimal CollateralAmount { get; set; }

        [StringLength(150, ErrorMessage = "Remarks cannot exceed 150 characters.")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; }


        public IEnumerable<SelectListItem> ProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ReceivePaymentDropdown { get; set; }

    }
}