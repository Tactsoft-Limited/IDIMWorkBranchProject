using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.BillType")]
    public partial class BillType
    {
        public BillType() { BillPayments = new HashSet<BillPayment>(); }

        [Key]
        public int BillTypeId { get; set; }

        [StringLength(255)]
        public string BillTypeName { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual ICollection<BillPayment> BillPayments { get; set; }
    }
}
