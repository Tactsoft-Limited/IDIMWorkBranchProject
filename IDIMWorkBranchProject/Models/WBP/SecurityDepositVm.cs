using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int SubProjectId { get; set; }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }

        [Display(Name = "Letter Date")]
        [DataType(DataType.Date)]
        public DateTime? LetterDate { get; set; }

        [Display(Name = "Letter No")]
        public string LetterNumber { get; set; }

        public double SecurityAmount { get; set; }

        public string Remark { get; set; }

        [ForeignKey(nameof(SubProjectId))]
        public virtual SubProjectVm SubProject { get; set; }

        public IEnumerable<SelectListItem> SubProjectDropdown { get; set; }
    }
}