using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.DirectReceivedDetail")]
    public partial class WorkBranchStoreDirectReceivedDetail
    {
        [Key]
        public int DirectReceivedDetailId { get; set; }

        public int DirectReceivedId { get; set; }

        public int ItemId { get; set; }

        public double ItemPrice { get; set; }

        public double Quantity { get; set; }

        [StringLength(50)]
        public string RackName { get; set; }

        public virtual WorkBranchStoreDirectReceived DirectReceived1 { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }
    }
}
