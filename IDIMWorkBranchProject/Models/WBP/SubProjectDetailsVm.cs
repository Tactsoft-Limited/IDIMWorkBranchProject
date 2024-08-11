using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SubProjectDetailsVm
    {
        public SubProjectDetailsVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
        }
        [Display(Name = "Id")]
        public int SubProjectDetailsId { get; set; }
        [Display(Name = "Sub Project")]
        public int SubProjectId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        [Display(Name = "NOA Completed")]
        public bool hasCompleted { get; set; }
        [Display(Name = "BG/PayOrder")]
        public string BGOrPayOrder { get; set; }
        [Display(Name = "BG Confirmed")]
        public bool BGConfirmed { get; set; }
        [Display(Name = "Contract Completed")]
        public bool ContractComplete { get; set; }
        [Display(Name = "Work Order Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WorkOrderDate { get; set; }
        [Display(Name = "Schedule & Drawing/Design Sent")]
        public bool DesignSent { get; set; }
        [Display(Name = "1st Installment")]
        public float Installment1 { get; set; }
        [Display(Name = "2nd Installment")]
        public float Installment2 { get; set; }
        [Display(Name = "4th Installment")]
        public float Installment4 { get; set; }
        [Display(Name = "5th Installment")]
        public float Installment5 { get; set; }
        [Display(Name = "Total Ammount")]
        public float TotalAmmount { get; set; }
        [Display(Name = "BGB Miscellaneous Fund Deposit")]
        public float BGBMiscellaneousDeposit { get; set; }
        [Display(Name = "Progress")]
        public int Progress { get; set; }
        [Display(Name = "Delivered")]
        public bool IsDelivered { get; set; }
        [Display(Name = "Paid Final Bill")]
        public bool IsFinalBillPaid { get; set; }
        [Display(Name = "Ten Percent Guarantee")]
        public bool TenPercentGuarantee { get; set; }
        [Display(Name = "Five Percent Security Money")]
        public bool FivePercentSecurityMoney { get; set; }
        [Display(Name = "Ten Percent Paid to Farm")]
        public float TenPercentPaidFarm { get; set; }
        [Display(Name = "Ten Percent Paid Simanto Fund")]
        public float TenPercentPaidSimantoFund { get; set; }
        [Display(Name = "Contractor Payable (without 10%/5%)")]
        public float ContractorPayable { get; set; }
        [Display(Name = "Remarks")]
        public string Remark { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
    }
}