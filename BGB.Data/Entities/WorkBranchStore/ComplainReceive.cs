using BGB.Data.Entities.Irms;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.ComplainReceive")]
    public partial class ComplainReceive
    {
        public int ComplainReceiveId { get; set; }

        public int ComplainId { get; set; }

        [StringLength(255)]
        public string ReceiveNo { get; set; }

        public DateTime ReceiveDate { get; set; }

        public int? ReceiveByArmyId { get; set; }

        public int ItemId { get; set; }

        public int Quantity { get; set; }

        public int QualityType { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual GeneralInformation GeneralInformation { get; set; }

        public virtual Complain Complain { get; set; }

        public virtual WorkBranchStoreItem Item2 { get; set; }
    }
}
