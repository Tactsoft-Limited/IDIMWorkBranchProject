using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.ComplainIssue")]
    public partial class ComplainIssue
    {
        public ComplainIssue() { ComplainIssueDetails = new HashSet<ComplainIssueDetail>(); }

        public int ComplainIssueId { get; set; }

        [StringLength(255)]
        public string ComplainIssueNo { get; set; }

        public DateTime ComplainIssueDate { get; set; }

        public int ComplainId { get; set; }

        public int IssueToArmyId { get; set; }

        public int IssueFromArmyId { get; set; }

        [Column(TypeName = "text")]
        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual GeneralInformation GeneralInformation { get; set; }

        public virtual GeneralInformation GeneralInformation1 { get; set; }

        public virtual Complain Complain { get; set; }

        public virtual ICollection<ComplainIssueDetail> ComplainIssueDetails { get; set; }
    }
}
