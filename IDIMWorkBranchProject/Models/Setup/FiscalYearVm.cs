using System;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.Setup
{
    public class FiscalYearVm
    {
        [Display(Name = "Id")]
        public int FiscalYearId { get; set; }

        [Display(Name = "Short Description")]
        public string FiscalYearDescription { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}