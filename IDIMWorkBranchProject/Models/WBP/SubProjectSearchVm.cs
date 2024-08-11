using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SubProjectSearchVm
    {
        public SubProjectSearchVm()
        {
            UnitDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
            SubProjects = new List<SubProjectVm>();
        }

        [Display(Name = "Project")]
        public string ProjectName { get; set; }
        [Display(Name = "Unit")]
        public int? UnitId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        [Display(Name = "Supplier")]
        public int? ConstructionFirmId { get; set; }
        public int? Status { get; set; }

        public IEnumerable<SelectListItem> UnitDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }

        public List<SubProjectVm> SubProjects { get; set; }
    }
}