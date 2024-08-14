using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.MaintenanceDemandIssue")]
    public partial class MaintenanceDemandIssue
    {
        public MaintenanceDemandIssue() { MaintenanceDemandIssueDetails = new HashSet<MaintenanceDemandIssueDetail>(); }

        public int MaintenanceDemandIssueId { get; set; }

        public int MaintenanceDemandId { get; set; }

        [StringLength(255)]
        public string MaintenanceDemandIssueNo { get; set; }

        public DateTime MaintenanceDemandIssueDate { get; set; }

        public int IssueUnitId { get; set; }

        [StringLength(255)]
        public string IssueAuthorizedBy { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual MaintenanceDemand MaintenanceDemand { get; set; }

        public virtual ICollection<MaintenanceDemandIssueDetail> MaintenanceDemandIssueDetails { get; set; }
    }
}
