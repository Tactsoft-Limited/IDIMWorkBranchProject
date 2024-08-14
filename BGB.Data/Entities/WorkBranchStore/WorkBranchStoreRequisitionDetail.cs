using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.RequisitionDetail")]
    public partial class WorkBranchStoreRequisitionDetail
    {
        [Key]
        public int RequisitionDetailId { get; set; }

        public int RequisitionId { get; set; }

        public int ItemId { get; set; }

        public double DemandQty { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }

        public virtual WorkBranchStoreRequisition Requisition1 { get; set; }
    }
}
