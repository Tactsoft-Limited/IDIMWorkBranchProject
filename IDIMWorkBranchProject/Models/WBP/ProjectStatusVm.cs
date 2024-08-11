using System;
using System.ComponentModel.DataAnnotations;

namespace IDIMWorkBranchProject.Models.WBP
{
    public class ProjectStatusVm
    {
        [Display(Name = "Id")]
        public int ProjectStatusId { get; set; }
        [Display(Name = "Sub Project")]
        public int SubProjectId { get; set; }
        [Display(Name = "Sub Project")]
        public string SubProjectTitle { get; set; }
        public string Progress { get; set; }

        [Display(Name = "Picture")]
        [UIHint("Picture")]
        public int? PictureId { get; set; }
        [Display(Name = "Picture")]
        public string PictureUrl { get; set; }

        [Display(Name = "Status Date")]
        [DataType(DataType.Date)]
        public DateTime? StatusDate { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}