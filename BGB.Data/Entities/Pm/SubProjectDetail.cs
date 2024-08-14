using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.SubProjectDetails")]
    public partial class SubProjectDetail
    {
        [Key]
        public int SubProjectDetailsId { get; set; }

        public int? SubProjectId { get; set; }

        public bool? hasCompleted { get; set; }

        [StringLength(50)]
        public string BGOrPayOrder { get; set; }

        public bool? BGConfirmed { get; set; }

        public bool? ContractComplete { get; set; }

        [Column(TypeName = "date")]
        public DateTime? WorkOrderDate { get; set; }

        public bool? DesignSent { get; set; }

        public double? Installment1 { get; set; }

        public double? Installment2 { get; set; }

        public double? Installment4 { get; set; }

        public double? Installment5 { get; set; }

        public double? TotalAmmount { get; set; }

        public double? BGBMiscellaneousDeposit { get; set; }

        public int? Progress { get; set; }

        public bool? IsDelivered { get; set; }

        public bool? IsFinalBillPaid { get; set; }

        public bool? TenPercentGuarantee { get; set; }

        public bool? FivePercentSecurityMoney { get; set; }

        public double? TenPercentPaidFarm { get; set; }

        public double? TenPercentPaidSimantoFund { get; set; }

        public double? ContractorPayable { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual SubProject SubProject { get; set; }
    }
}
