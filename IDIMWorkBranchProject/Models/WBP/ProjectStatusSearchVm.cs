using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ProjectStatusSearchVm
    {
        public ProjectStatusSearchVm()
        {
            ProjectStatus = new List<ProjectStatusVm>();
        }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        public List<ProjectStatusVm> ProjectStatus { get; set; }
    }
}