using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ProjectExtendSearchVm
    {
        public ProjectExtendSearchVm()
        {
            ProjectExtends = new List<ProjectExtendVm>();
        }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        public List<ProjectExtendVm> ProjectExtends { get; set; }
    }
}