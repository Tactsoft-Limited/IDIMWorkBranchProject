using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SubProjectDetailsSearchVm
    {
        public SubProjectDetailsSearchVm()
        {
            SubProjectDetails = new List<SubProjectDetailsVm>();
            SubProjectDropdown = new List<SelectListItem>();
        }

        [Display(Name = "Sub Project")]
        public int? SubProjectId { get; set; }

        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public List<SubProjectDetailsVm> SubProjectDetails { get; set; }
    }
}