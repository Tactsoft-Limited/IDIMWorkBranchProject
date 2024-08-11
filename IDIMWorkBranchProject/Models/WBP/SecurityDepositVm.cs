using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SecurityDepositVm
    {
        public SecurityDepositVm()
        {
            SubProjectDropdown = new List<SelectListItem>();
        }
        [Display(Name = "Id")]
        public int SecurityDepositId { get; set; }
        [Display(Name = "Sub Project")]
        public Nullable<int> SubProjectId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        public Nullable<double> Amount { get; set; }
        [Display(Name = "Letter Date")]
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> Letterdate { get; set; }
        [Display(Name = "Letter No")]
        public string Letterno { get; set; }
        public string Remark { get; set; }


        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
    }
}