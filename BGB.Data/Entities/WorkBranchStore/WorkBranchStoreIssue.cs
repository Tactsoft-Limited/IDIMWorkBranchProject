using BGB.Data.Entities.Inv;
using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Issue")]
    public partial class WorkBranchStoreIssue
    {
        public WorkBranchStoreIssue() { IssueDetail2 = new HashSet<WorkBranchStoreIssueDetail>(); }

        [Key]
        public int IssueId { get; set; }

        public int StoreId { get; set; }

        public int? RequisitionId { get; set; }

        [StringLength(50)]
        public string IssueSerial { get; set; }

        [StringLength(50)]
        public string IssueReferenceNo { get; set; }

        public DateTime DateOfIssue { get; set; }

        public int UnitId { get; set; }

        [StringLength(150)]
        public string RequisitionNoteSheet { get; set; }

        [StringLength(50)]
        public string IssuedBy { get; set; }

        [StringLength(10)]
        public string IssueStatus { get; set; }

        [StringLength(50)]
        public string VoucherNo { get; set; }

        public DateTime? VoucherDate { get; set; }

        public bool Flag { get; set; }

        [StringLength(150)]
        public string Remarks { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Store Store { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual WorkBranchStoreRequisition Requisition1 { get; set; }

        public virtual ICollection<WorkBranchStoreIssueDetail> IssueDetail2 { get; set; }
    }
}
