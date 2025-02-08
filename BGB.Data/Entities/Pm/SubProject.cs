using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.SubProject")]
    public partial class SubProject
    {
        public SubProject()
        {
            BillPayments = new HashSet<BillPayment>();
            ProjectStatus = new HashSet<ProjectStatus>();
            ContractorPayments = new HashSet<ContractorPayment>();
        }

        [Key]
        public int SubProjectId { get; set; }
        public int ProjectId { get; set; }

        [StringLength(255)]
        public string SubProjectTitle { get; set; }
        public string Description { get; set; }
        public int ConstructionFirmId { get; set; }
        public decimal AgreementCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime HandOverDate { get; set; }
        public string BankGuarantee { get; set; }
        public DateTime? BankGuaranteeEndDate { get; set; }
        public string NOA { get; set; }
        public DateTime? NOADate { get; set; }
        public string Agreement { get; set; }
        public DateTime? AgreementDate { get; set; }
        public string WorkOrder { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public string Remark { get; set; }
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
        public virtual ICollection<BillPayment> BillPayments { get; set; }
        public virtual ICollection<ProjectStatus> ProjectStatus { get; set; }
        public virtual ICollection<ContractorPayment> ContractorPayments { get; set; }

        #endregion
    }
}
