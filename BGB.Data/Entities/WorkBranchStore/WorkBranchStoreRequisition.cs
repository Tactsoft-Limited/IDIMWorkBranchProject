using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.Requisition")]
    public partial class WorkBranchStoreRequisition
    {
        public WorkBranchStoreRequisition()
        {
            Issue2 = new HashSet<WorkBranchStoreIssue>();
            RequisitionDetail1 = new HashSet<WorkBranchStoreRequisitionDetail>();
        }

        [Key]
        public int RequisitionId { get; set; }

        [StringLength(150)]
        public string RequisitionNo { get; set; }

        public DateTime RequisitionDate { get; set; }

        public int UnitId { get; set; }

        [StringLength(50)]
        public string AuthorizedBy { get; set; }

        [StringLength(150)]
        public string NoteSheetAttached { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public bool IsComplete { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual ICollection<WorkBranchStoreIssue> Issue2 { get; set; }

        public virtual ICollection<WorkBranchStoreRequisitionDetail> RequisitionDetail1 { get; set; }
    }
}
