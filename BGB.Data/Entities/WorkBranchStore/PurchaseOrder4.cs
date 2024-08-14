using BGB.Data.Entities.Inv;
using System;
using System.Collections.Generic;
using BGB.Data.Entities.Irms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.PurchaseOrder")]
    public partial class PurchaseOrder4
    {
        public PurchaseOrder4()
        {
            PurchaseOrderDetail4 = new HashSet<PurchaseOrderDetail4>();
            PurchaseOrderReceived1 = new HashSet<PurchaseOrderReceived1>();
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

        public virtual ICollection<PurchaseOrderDetail4> PurchaseOrderDetail4 { get; set; }

        public virtual ICollection<PurchaseOrderReceived1> PurchaseOrderReceived1 { get; set; }
    }
}
