using BGB.Data.Entities.Budget;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.MaintenanceDemandIssueDetail")]
    public partial class MaintenanceDemandIssueDetail
    {
        public int MaintenanceDemandIssueDetailId { get; set; }

        public int MaintenanceDemandIssueId { get; set; }

        public int AccountCodeId { get; set; }

        public double IssueAmount { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual AccountCode AccountCode { get; set; }

        public virtual MaintenanceDemandIssue MaintenanceDemandIssue { get; set; }
    }
}
