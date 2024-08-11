using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ConsultantSearchVm
    {
        public ConsultantSearchVm()
        {
            Consultant = new List<ConsultantVm>();
        }

        [Display(Name = "Consultant Name")]
        public string Name { get; set; }
        public List<ConsultantVm> Consultant { get; set; }
    }
}