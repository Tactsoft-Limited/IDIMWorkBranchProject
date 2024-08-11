using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class SubProjectVm
    {
        public SubProjectVm()
        {
            UnitDropdown = new List<SelectListItem>();
            ConstructionFirmDropdown = new List<SelectListItem>();
        }

        [Display(Name = "Id")]
        public int SubProjectId { get; set; }

        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [Display(Name = "Project")]
        public string ProjectName { get; set; }

        [Display(Name = "Unit")]
        public int UnitId { get; set; }
        [Display(Name = "Unit")]
        public string UnitName { get; set; }

        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }

        [Display(Name = "Construction Firm")]
        public int ConstructionFirmId { get; set; }
        [Display(Name = "Construction Firm")]
        public string ConstructionFirmName { get; set; }

        public string Description { get; set; }

        [Display(Name = "Agreement Cost")]
        public double AgreementCost { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        [Display(Name = "Status")]
        public StatusType StatusTypeId { get; set; }
        [Display(Name = "Progress Status")]
        public int Status { get; set; }
        [DataType(DataType.Date)]

        [Display(Name = "Hand Over Date")]
        public DateTime? HandOverDate { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }

        public IEnumerable<SelectListItem> UnitDropdown { get; set; }
        public IEnumerable<SelectListItem> ConstructionFirmDropdown { get; set; }
    }
}