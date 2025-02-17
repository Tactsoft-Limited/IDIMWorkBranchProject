using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ReceivePayment")]
    public partial class ReceivePayment
    {
        public ReceivePayment()
        {
            VatTaxes = new HashSet<VatTax>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceivePaymentId { get; set; }

        public int ProjectId { get; set; }

        public int ConstructionFirmId { get; set; }

        [MaxLength(150)]
        public string LetterNo { get; set; }

        public DateTime BillDate { get; set; }

        public DateTime ExtraTime { get; set; }

        [MaxLength(200)]
        public string BillPaymentSector { get; set; }

        public decimal Budget { get; set; }

        public int WorkProgress { get; set; }

        public double FinancialProgress { get; set; }

        public int BillNumber { get; set; }

        public int BillPercentage { get; set; }

        public decimal BillAmount { get; set; }

        public string Remarks { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }


        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        [ForeignKey(nameof(ConstructionFirmId))]
        public virtual ConstructionFirm ConstructionFirm { get; set; }

        #region Navigation Properties
        public virtual ICollection<VatTax> VatTaxes { get; set; }
        #endregion
    }

}
