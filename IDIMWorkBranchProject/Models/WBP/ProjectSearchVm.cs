using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ProjectSearchVm : DataTablaVm
    {
        public ProjectSearchVm()
        {
            AuthorizeUnitDropdown = new List<SelectListItem>();
            FiscalYearDropdown = new List<SelectListItem>();
            ProjectTypeDropdown = new List<SelectListItem>();
            Projects = new List<ProjectVm>();
        }

        [Display(Name = "Project Type")]
        public int? ProjectTypeId { get; set; }

        [Display(Name = "Fiscal Year")]
        public int? FiscalYearId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }

        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
        public IEnumerable<SelectListItem> ProjectTypeDropdown { get; set; }
        public IEnumerable<SelectListItem> AuthorizeUnitDropdown { get; set; }

        public List<ProjectVm> Projects { get; set; }
    }


}