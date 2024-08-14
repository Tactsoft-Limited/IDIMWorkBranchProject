using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.ComplainIssueDetail")]
    public partial class ComplainIssueDetail
    {
        public int ComplainIssueDetailId { get; set; }

        public int ComplainIssueId { get; set; }

        public int ItemId { get; set; }

        public double ReturnQuantity { get; set; }

        public double IssueQuantity { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ComplainIssue ComplainIssue { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }
    }
}
