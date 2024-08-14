using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.PurchaseOrderReceivedDetail")]
    public partial class PurchaseOrderReceivedDetail1
    {
        [Key]
        public int ReceivedDetailId { get; set; }

        public int ReceivedId { get; set; }

        public int ItemId { get; set; }

        public double Quantity { get; set; }

        [StringLength(50)]
        public string RackName { get; set; }

        [StringLength(50)]
        public string Remarks { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }

        public virtual PurchaseOrderReceived1 PurchaseOrderReceived1 { get; set; }
    }
}
