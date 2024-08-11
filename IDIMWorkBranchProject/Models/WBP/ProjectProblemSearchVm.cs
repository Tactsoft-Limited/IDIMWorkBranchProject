using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ProjectProblemSearchVm
    {
        public ProjectProblemSearchVm()
        {
            ProjectProblems = new List<ProjectProblemVm>();
        }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        public List<ProjectProblemVm> ProjectProblems { get; set; }
    }
}