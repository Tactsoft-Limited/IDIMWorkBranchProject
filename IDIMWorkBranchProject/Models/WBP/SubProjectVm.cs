using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SubProjectVm
    {
        public SubProjectVm()
        {
            UnitDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
        }

        public int SubProjectId { get; set; }

        [Display(Name = "Project Name")]
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }

        [Display(Name = "Construction Firm")]
        public int ConstructionFirmId { get; set; }

        [Display(Name = "Construction Firm")]
        public string ConstructionFirmName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Agreement Cost")]
        public decimal AgreementCost { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Hand Over Date")]
        [DataType(DataType.Date)]
        public DateTime HandOverDate { get; set; }


        [Display(Name = "Bank Guarantee")]
        public string BankGuarantee { get; set; }

        [Display(Name = "Bank Guarantee")]
        public HttpPostedFileBase BankGuaranteeFile { get; set; }

        [Display(Name = "Bank Guarantee End Date")]
        [DataType(DataType.Date)]
        public DateTime BankGuaranteeEndDate { get; set; }

        public string NOA { get; set; }

        [Display(Name = "NOA")]
        public HttpPostedFileBase NOAFile { get; set; }

        [Display(Name = "NOA Date")]
        [DataType(DataType.Date)]
        public DateTime NOADate { get; set; }

        public string Agreement { get; set; }

        [Display(Name = "Agreement")]
        public HttpPostedFileBase AgreementFile { get; set; }

        [Display(Name = "Agreement Date")]
        [DataType(DataType.Date)]
        public DateTime AgreementDate { get; set; }

        [Display(Name = "Work Order")]
        public string WorkOrder { get; set; }

        [Display(Name = "Work Order")]
        public HttpPostedFileBase WorkOrderFile { get; set; }

        [Display(Name = "Work Order Date")]
        [DataType(DataType.Date)]
        public DateTime WorkOrderDate { get; set; }

        public string Remark { get; set; }

        public IEnumerable<SelectListItem> UnitDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }
    }
}