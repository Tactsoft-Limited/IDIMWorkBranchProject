using System.ComponentModel.DataAnnotations;
using System;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class BGBFundVm
    {
        public int BGBFundId { get; set; }

        [Required(ErrorMessage = "Letter Number is required.")]
        [StringLength(150, ErrorMessage = "Letter Number cannot exceed 150 characters.")]
        public string LetterNo { get; set; }

        [Required(ErrorMessage = "Deposit Date is required.")]
        [Display(Name = "Deposit Date")]
        public DateTime DepositeDate { get; set; }

        [Required(ErrorMessage = "Project Id is required.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Construction Firm Id is required.")]
        public int ConstructionFirmId { get; set; }

        [Required(ErrorMessage = "Pay Order Number is required.")]
        [StringLength(150, ErrorMessage = "Pay Order Number cannot exceed 150 characters.")]
        public string PayOrderNo { get; set; }

        [Required(ErrorMessage = "Pay Order Date is required.")]
        [Display(Name = "Pay Order Date")]
        public DateTime PayOrderDate { get; set; }

        [Required(ErrorMessage = "Bank Name is required.")]
        [StringLength(150, ErrorMessage = "Bank Name cannot exceed 150 characters.")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Branch Name is required.")]
        [StringLength(150, ErrorMessage = "Branch Name cannot exceed 150 characters.")]
        public string BrunchName { get; set; }

        [Required(ErrorMessage = "Account Number is required.")]
        [StringLength(150, ErrorMessage = "Account Number cannot exceed 150 characters.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public decimal Amount { get; set; }

        [StringLength(150, ErrorMessage = "Remarks cannot exceed 150 characters.")]
        public string Remarks { get; set; }


    }
}