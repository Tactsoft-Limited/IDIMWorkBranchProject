using System;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ProjectExtendVm
    {
        [Display(Name = "Id")]
        public int ProjectExtendId { get; set; }
        [Display(Name = "Sub Project")]
        public int SubProjectId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        [Display(Name = "No Of Days")]
        public int NoOfDays { get; set; }

        [Display(Name = "Extend Date")]
        [DataType((DataType.Date))]
        public DateTime? ExtendDate { get; set; }
        [Display(Name = "New Handover Date")]
        [DataType((DataType.Date))]
        public DateTime? NewHandOverDate { get; set; }

        [Display(Name = "Picture")]
        [UIHint("Picture")]
        public int? AttachmentId { get; set; }
        
        public string Reason { get; set; }
        public string Remark { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}