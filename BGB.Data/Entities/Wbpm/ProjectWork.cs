using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ProjectWork", Schema = "wbpm")]
    public class ProjectWork : BaseEntity
    {
        public ProjectWork()
        {
            ContractorCompanyPayments = new HashSet<ContractorCompanyPayment>();
            ADPReceivePayments = new HashSet<ADPReceivePayment>();
            BGBMiscellaneousFunds = new HashSet<BGBMiscellaneousFund>();
            Nohas = new HashSet<Noha>();
            PerformanceSecurities = new HashSet<PerformanceSecurity>();
            ProjectWorkStatuses = new HashSet<ProjectWorkStatus>();
            WorkOrders = new HashSet<WorkOrder>();
            ContractAgreements = new HashSet<ContractAgreement>();
        }


        [Key]
        public int ProjectWorkId { get; set; }
        public int ADPProjectId { get; set; }
        public string ProjectWorkTitle { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public decimal? EstimatedCost { get; set; }
        public string EstimatedCostInWord { get; set; }
        public string EstimatedCostInWordBangla { get; set; }
        public bool IsFurnitureIncluded { get; set; }
        public bool IsNoahCompleted { get; set; }
        public bool IsPerformanceSecuritySubmited { get; set; }
        public bool IsAgreementCompleted { get; set; }
        public bool IsWorkOrderCompleted { get; set; }       
        public bool IsFinalBillSubmitted { get; set; }
        public string Remarks { get; set; }


        #region Navigation Properties
        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }

        public virtual ICollection<Noha> Nohas { get; set; }
        public virtual ICollection<ContractAgreement> ContractAgreements { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
        public virtual ICollection<PerformanceSecurity> PerformanceSecurities { get; set; }
        public virtual ICollection<ProjectWorkStatus> ProjectWorkStatuses { get; set; }
        public virtual ICollection<ContractorCompanyPayment> ContractorCompanyPayments { get; set; }
        public virtual ICollection<ADPReceivePayment> ADPReceivePayments { get; set; }
        public virtual ICollection<BGBMiscellaneousFund> BGBMiscellaneousFunds { get; set; }
        #endregion
    }
}
