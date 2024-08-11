using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ConsultancyFeesSearchVm
    {
        public ConsultancyFeesSearchVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
            ConsultantDropdown = new List<SelectListItem>();
            ConsultancyFees = new List<ConsultancyFeesVm>();
        }

        [Display(Name = "Sub Project")]
        public int? SubProjectId { get; set; }

        [Display(Name = "Consultant")]
        public int? ConsultantId { get; set; }

        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ConsultantDropdown { get; set; }
        public List<ConsultancyFeesVm> ConsultancyFees { get; set; }
    }
}