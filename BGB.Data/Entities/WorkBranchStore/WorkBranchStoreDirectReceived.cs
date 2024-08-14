using BGB.Data.Entities.Inv;
using BGB.Data.Entities.Irms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.DirectReceived")]
    public partial class WorkBranchStoreDirectReceived
    {
        public WorkBranchStoreDirectReceived()
        { DirectReceivedDetail1 = new HashSet<WorkBranchStoreDirectReceivedDetail>(); }

        [Key]
        public int DirectReceivedId { get; set; }

        public int? SupplierId { get; set; }

        public DateTime DateOfReceived { get; set; }

        public int UnitId { get; set; }

        public int? StoreId { get; set; }

        [StringLength(150)]
        public string DirectRecievedChallan { get; set; }

        [StringLength(50)]
        public string AutherizedBy { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Store Store { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual ICollection<WorkBranchStoreDirectReceivedDetail> DirectReceivedDetail1 { get; set; }
    }
}
