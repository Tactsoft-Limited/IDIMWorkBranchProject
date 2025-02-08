using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{

    public class ProjectVm
    {
        public ProjectVm()
        {
            ProjectTypeDropdown = new List<SelectListItem>();
            FiscalYearDropdown = new List<SelectListItem>();
        }

        public int ProjectId { get; set; }

        [DisplayName("Fiscal Year")]
        public int? FiscalYearId { get; set; }

        [DisplayName("Fiscal Year")]
        public string FiscalYearDescription { get; set; }

        [Required(ErrorMessage = "Project Name is required.")]
        [StringLength(255, ErrorMessage = "Project Name cannot exceed 255 characters.")]
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }

        [DisplayName("Project Type")]
        public int ProjectTypeId { get; set; }

        public string ProjectTypeName { get; set; }

        [StringLength(255, ErrorMessage = "Ministry/Department cannot exceed 255 characters.")]
        [DisplayName("Ministry/Department")]
        public string MinistryDepartment { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Estimated Expenses must be a positive number.")]
        [DisplayName("Estimated Expenses")]
        public decimal EstimatedExpenses { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime EndingDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of Works must be a non-negative number.")]
        [DisplayName("No Of Work")]
        public int NoOfWork { get; set; }

        [Range(0, 100, ErrorMessage = "Economic Progress must be between 0 and 100.")]
        [DisplayName("Economic Progress")]
        public decimal EconomicProgress { get; set; }

        [Range(0, 100, ErrorMessage = "Construction Progress must be between 0 and 100.")]
        [DisplayName("Construction Progress")]
        public decimal ConstructionProgress { get; set; }

        [StringLength(255, ErrorMessage = "PD cannot exceed 255 characters.")]
        [DisplayName("Project Director")]
        public string PD { get; set; }

        [StringLength(255, ErrorMessage = "DPD cannot exceed 255 characters.")]
        [DisplayName("Deputy Project Director")]
        public string DPD { get; set; }

        [StringLength(250, ErrorMessage = "Remarks cannot exceed 250 characters.")]
        public string Remarks { get; set; }


        public IEnumerable<SelectListItem> ProjectTypeDropdown { get; set; }
        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
    }


}
