using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.IssueDetail")]
    public partial class WorkBranchStoreIssueDetail
    {
        [Key]
        public int IssueDetailId { get; set; }

        public int IssueId { get; set; }

        public int ItemId { get; set; }

        public double? UsableQty { get; set; }

        public double? PartialUsableQty { get; set; }

        public double? DamageQty { get; set; }

        public double? TotalQty { get; set; }

        public virtual WorkBranchStoreIssue Issue2 { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }
    }
}
