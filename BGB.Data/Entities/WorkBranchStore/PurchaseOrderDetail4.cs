using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.PurchaseOrderDetail")]
    public partial class PurchaseOrderDetail4
    {
        [Key]
        public int PurchaseOrderDetailId { get; set; }

        public int PurchaseOrderId { get; set; }

        public int ItemId { get; set; }

        public double ItemPrice { get; set; }

        public double Quantity { get; set; }

        [StringLength(150)]
        public string Remarks { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }

        public virtual PurchaseOrder4 PurchaseOrder4 { get; set; }
    }
}
