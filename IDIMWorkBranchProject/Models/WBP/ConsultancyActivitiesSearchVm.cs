using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ConsultancyActivitiesSearchVm
    {
        public ConsultancyActivitiesSearchVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
            ConsultantDropdown = new List<SelectListItem>();
            ConsultancyActivities = new List<ConsultancyActivitiesVm>();
        }

        [Display(Name = "Sub Project")]
        public int? SubProjectId { get; set; }

        [Display(Name = "Consultant")]
        public int? ConsultantId { get; set; }

        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
        public IEnumerable<SelectListItem> ConsultantDropdown { get; set; }
        public List<ConsultancyActivitiesVm> ConsultancyActivities { get; set; }
    }
}