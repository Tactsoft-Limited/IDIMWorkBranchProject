using BGB.Data.Entities.Inv;
using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.PurchaseOrder")]
    public partial class WorkBranchStorePurchaseOrder
    {
        public WorkBranchStorePurchaseOrder()
        {
            PurchaseOrderDetail4 = new HashSet<WorkBranchStorePurchaseOrderDetail>();
            PurchaseOrderReceived1 = new HashSet<WorkBranchStorePurchaseOrderReceived>();
        }

        [Key]
        public int PurchaseOrderId { get; set; }

        [StringLength(50)]
        public string PurchaseOrderNo { get; set; }

        [StringLength(50)]
        public string TenderScheduleNo { get; set; }

        public int DelivaryUnitId { get; set; }

        public int? IssueUnitId { get; set; }

        public int SupplierId { get; set; }

        public DateTime? PODate { get; set; }

        [StringLength(50)]
        public string PONoteSheetNo { get; set; }

        [StringLength(250)]
        public string PONoteSheetAttachedDoc { get; set; }

        public double? POTotalValue { get; set; }

        public DateTime? PODalivaryDate { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public bool IsComplete { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual IrmsSetupUnit DelivaryIrmsSetupUnit { get; set; }

        public virtual IrmsSetupUnit IssueIrmsSetupUnit { get; set; }

        public virtual ICollection<WorkBranchStorePurchaseOrderDetail> PurchaseOrderDetail4 { get; set; }

        public virtual ICollection<WorkBranchStorePurchaseOrderReceived> PurchaseOrderReceived1 { get; set; }
    }
}
