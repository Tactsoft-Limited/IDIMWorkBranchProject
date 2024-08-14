using BGB.Data.Entities.Inv;
using System;
using System.Collections.Generic;
using BGB.Data.Entities.Irms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.PurchaseOrderReceived")]
    public partial class PurchaseOrderReceived1
    {
        public PurchaseOrderReceived1()
        {
            PurchaseOrderReceivedDetail1 = new HashSet<PurchaseOrderReceivedDetail1>();
        }

        [Key]
        public int ReceivedId { get; set; }

        public DateTime DateOfReceived { get; set; }

        public int? PurchaseOrderId { get; set; }

        [StringLength(50)]
        public string RecievedChallan { get; set; }

        public int UnitId { get; set; }

        public int StoreId { get; set; }

        [StringLength(50)]
        public string ReceivedBy { get; set; }

        [StringLength(250)]
        public string ChallanAttached { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Store Store { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual PurchaseOrder4 PurchaseOrder4 { get; set; }

        public virtual ICollection<PurchaseOrderReceivedDetail1> PurchaseOrderReceivedDetail1 { get; set; }
    }
}
