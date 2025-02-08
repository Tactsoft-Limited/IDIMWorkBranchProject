using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SubProjectSearchVm : DataTablaVm
    {
        public SubProjectSearchVm()
        {
            UnitDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
            SubProjects = new List<SubProjectVm>();
        }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Firm Name")]
        public int? ConstructionFirmId { get; set; }

        public int UnitId { get; set; }

        public IEnumerable<SelectListItem> UnitDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }

        public List<SubProjectVm> SubProjects { get; set; }
    }
}