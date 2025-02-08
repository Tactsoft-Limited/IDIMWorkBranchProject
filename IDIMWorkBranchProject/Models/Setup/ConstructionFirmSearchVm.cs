using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Setup
{
    public class ConstructionFirmSearchVm : DataTablaVm
    {
        [Display(Name = " Company Name")]
        public string ConstructionFirmName { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactPhone { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }


        public List<ConstructionFirmVm> ConstructionFirms { get; set; }
    }
}