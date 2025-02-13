using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
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
            ContractAgreements = new HashSet<ContractAgreement>();
        }


        [Key]
        public int ProjectWorkId { get; set; }

        public int ADPProjectId { get; set; }
        public int ConstructionCompanyId { get; set; }
        public string ProjectWorkTitle { get; set; }
        public decimal? EstimatedCost { get; set; }
        public DateTime NOADate { get; set; }
        public DateTime AgreementDate { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public DateTime WorkStartDate { get; set; }
        public DateTime WorkEndDate { get; set; }
        public DateTime? OnFieldStartDate { get; set; }
        public DateTime? OnFieldEndDate { get; set; }
        public DateTime HandOverDate { get; set; }
        public DateTime BankGuaranteeEndDate { get; set; }
        public double ConstructionProgress { get; set; }
        public string BankGuaranteeDocument { get; set; }
        public string NOADocument { get; set; }
        public string AgreementDocument { get; set; }
        public string WorkOrderDocument { get; set; }
        public string WorkStatus { get; set; }
        public string Remarks { get; set; }

        #region Navigation Properties
        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }
        [ForeignKey(nameof(ConstructionCompanyId))]
        public virtual ConstructionCompany ConstructionCompany { get; set; }


        public virtual ICollection<ContractorCompanyPayment> ContractorCompanyPayments { get; set; }
        public virtual ICollection<ADPReceivePayment> ADPReceivePayments { get; set; }
        public virtual ICollection<BGBMiscellaneousFund> BGBMiscellaneousFunds { get; set; }
        public virtual ICollection<ContractAgreement> ContractAgreements { get; set; }
        #endregion
    }
}
