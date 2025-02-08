using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ConsultancyFees")]
    public partial class ConsultancyFee
    {
        [Key]
        public int CFId { get; set; }

        public int? SubProjectId { get; set; }

        public int? ConsultantId { get; set; }

        [Column("ConsultancyFee")]
        public double? ConsultancyFee1 { get; set; }

        public double? Vat_Tax { get; set; }

        public double? NitAmmountConsultancyFee { get; set; }

        public double? OtherConsultancyFee { get; set; }

        public double? RestAmount { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Consultant Consultant { get; set; }

        public virtual SubProject SubProject { get; set; }
    }
}
