using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.Project")]
    public partial class Project
    {
        public Project()
        {
            PicMeetings = new HashSet<PicMeeting>();
            ReceiptPayments = new HashSet<ReceiptPayment>();
            SubProjects = new HashSet<SubProject>();
        }

        public int ProjectId { get; set; }

        public int AuthorizeUnitId { get; set; }

        public int FiscalYearId { get; set; }

        public int DevelopmentTypeId { get; set; }

        [StringLength(255)]
        public string ProjectCode { get; set; }

        [Required]
        [StringLength(255)]
        public string ProjectName { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime? ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        [StringLength(255)]
        public string ProjectDirector { get; set; }

        public double BudgetCapital { get; set; }

        public double BudgetRevenue { get; set; }

        public string Description { get; set; }

        [StringLength(255)]
        public string PicMeetingNo { get; set; }

        public string PicMeetingDescriptioin { get; set; }

        public string PscMeetingDescription { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<PicMeeting> PicMeetings { get; set; }

        public virtual ICollection<ReceiptPayment> ReceiptPayments { get; set; }

        public virtual ICollection<SubProject> SubProjects { get; set; }
    }
}
