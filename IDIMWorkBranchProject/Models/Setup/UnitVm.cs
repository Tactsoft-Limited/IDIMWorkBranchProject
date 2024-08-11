using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using IDIMWorkBranchProject.Models.Validation.Setup;

namespace IDIMWorkBranchProject.Models.Setup
{
    [Validator(typeof(SetupUnitVmValidator))]
    public class UnitVm
    {
        [Display(Name = "Id")]
        public int UnitId { get; set; }

        [Display(Name = "Name")]
        public string UnitName { get; set; }

        [Display(Name = "Name (Bangla)")]
        public string UnitNameB { get; set; }

        [Display(Name = "Unit Full Name")]
        public string UnitFullName { get; set; }

        [Display(Name = "Sub Organize")]
        public int? SubOrganizationId { get; set; }

        [Display(Name = "Code")]
        public string UnitCode { get; set; }
       
        [Display(Name = "Core")]
        public int? CoreId { get; set; }

        [Display(Name = "Place")]
        public int? PlaceId { get; set; }


        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Israb")]
        public int? Israb { get; set; }

        [Display(Name = "Sector")]
        public int? SectorId { get; set; }

        [Display(Name = "Region")]
        public int? RegionId { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}