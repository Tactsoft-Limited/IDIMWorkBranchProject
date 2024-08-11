using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ProjectSearchVm
    {
        public ProjectSearchVm()
        {
            FiscalYearDropdown = new List<SelectListItem>();
            AuthorizeUnitDropdown = new List<SelectListItem>();
            Projects = new List<ProjectVm>();
        }

        [Display(Name = "Authorize Unit")]
        public int? AuthorizeUnitId { get; set; }

        [Display(Name = "Fiscal Year")]
        public int? FiscalYearId { get; set; }

        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        public IEnumerable<SelectListItem> FiscalYearDropdown { get; set; }
        public IEnumerable<SelectListItem> AuthorizeUnitDropdown { get; set; }

        public List<ProjectVm> Projects { get; set; }
    }
}