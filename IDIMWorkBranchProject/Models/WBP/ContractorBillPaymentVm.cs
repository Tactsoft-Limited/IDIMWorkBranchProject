using System.ComponentModel.DataAnnotations;
using System;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ContractorBillPaymentVm
    {
        public int ContractorBillPaymentId { get; set; }

        [Required(ErrorMessage = "Letter Number is required.")]
        [StringLength(150, ErrorMessage = "Letter Number cannot exceed 150 characters.")]
        public string LetterNo { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Bill Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Bill Amount must be a positive number.")]
        public decimal BillAmount { get; set; }

        [Required(ErrorMessage = "Check Number is required.")]
        [StringLength(150, ErrorMessage = "Check Number cannot exceed 150 characters.")]
        public string CheckNumber { get; set; }

        [Required(ErrorMessage = "Bank Name is required.")]
        [StringLength(150, ErrorMessage = "Bank Name cannot exceed 150 characters.")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Branch Name is required.")]
        [StringLength(150, ErrorMessage = "Branch Name cannot exceed 150 characters.")]
        public string BrunchName { get; set; }

        [Required(ErrorMessage = "Account Number is required.")]
        [StringLength(150, ErrorMessage = "Account Number cannot exceed 150 characters.")]
        public string AccountNumber { get; set; }

        [StringLength(150, ErrorMessage = "Remarks cannot exceed 150 characters.")]
        public string Remarks { get; set; }


    }
}