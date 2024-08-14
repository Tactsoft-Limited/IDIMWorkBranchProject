using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.SecurityDeposit")]
    public partial class PmSecurityDeposit
    {
        [Key]
        public int SecurityDepositId { get; set; }

        public int? SubProjectId { get; set; }

        public double? SecurityAmount { get; set; }

        public DateTime? LetterDate { get; set; }

        [StringLength(100)]
        public string LetterNumber { get; set; }

        public string Remark { get; set; }

        [ForeignKey(nameof(SubProjectId))]
        public virtual SubProject SubProject { get; set; }
    }
}
