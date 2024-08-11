using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.WBP;

namespace IDIMWorkBranchProject.Models.WBP
{
    [Validator(validatorType: typeof(ProjectVmValidator))]
    public class ProjectVm
    {
        public ProjectVm()
        {
            AuthorizeUnitDropdown = new List<SelectListItem>();
            FiscalYearDropdown = new List<SelectListItem>();
        }

        [Display(Name = "Id")]
        public int ProjectId { get; set; }

        [Display(Name = "Authorize Unit")]
        public int AuthorizeUnitId { get; set; }
        [Display(Name = "Authorize Unit")]
        public string AuthorizeUnitName { get; set; }
        [Display(Name = "Budget Type")]
        public DevelopmentType DevelopmentTypeId { get; set; }
        [Display(Name = "Financial Year")]
        public int FiscalYearId { get; set; }
        [Display(Name = "Financial Year")]
        public string FiscalYearName { get; set; }
        [Display(Name = "Code")]
        public string ProjectCode { get; set; }
        [Display(Name = "Project")]
        public string ProjectName { get; set; }

        [Display(Name = "Approval Date")]
        [DataType(DataType.Date)]
        public DateTime? ApprovalDate { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? ProjectStartDate { get; set; }
        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        public DateTime? ProjectEndDate { get; set; }

        [Display(Name = "Project Director")]
        public string ProjectDirector { get; set; }
        [Display(Name = "Allotment")]
        public double BudgetCapital { get; set; }
        [Display(Name = "Fund Release")]
        public double BudgetRevenue { get; set; }
        public string Description { get; set; }
        [Display(Name = "PIC Meeting")]
        public string PicMeetingNo { get; set; }

        public Nullable<double> Salendar { get; set; }
        public Nullable<double> Expenditure { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        public IEnumerable<SelectListItem> AuthorizeUnitDropdown { get; set; }
        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
    }
}
