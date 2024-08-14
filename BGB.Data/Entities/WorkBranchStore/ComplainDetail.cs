using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.ComplainDetail")]
    public partial class ComplainDetail
    {
        public int ComplainDetailId { get; set; }

        public int ComplainId { get; set; }

        public int ItemId { get; set; }

        public double Quantity { get; set; }

        public string Description { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Complain Complain { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }
    }
}
