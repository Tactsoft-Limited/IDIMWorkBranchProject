using BGB.Data.Entities.Budget;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.Project")]
    public partial class Project
    {
        public Project()
        {
            ReceivePayments = new HashSet<ReceivePayment>();
            SubProjects = new HashSet<SubProject>();
            VatTaxes = new HashSet<VatTax>();
            BGBFunds = new HashSet<BGBFund>();
        }

        [Key]
        public int ProjectId { get; set; }
        public int? FiscalYearId { get; set; }
        public int ProjectTypeId { get; set; }
        public string ProjectName { get; set; }
        public string MinistryDepartment { get; set; }
        public decimal EstimatedExpenses { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int NoOfWork { get; set; }
        public decimal EconomicProgress { get; set; }
        public decimal ConstructionProgress { get; set; }
        public string PD { get; set; }
        public string DPD { get; set; }
        public string Remarks { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int? UpdateNo { get; set; }

        [ForeignKey(nameof(FiscalYearId))]
        public FiscalYear FiscalYear { get; set; }

        [ForeignKey(nameof(ProjectTypeId))]
        public ProjectType ProjectType { get; set; }

        #region Navigation Properties
        public virtual ICollection<ReceivePayment> ReceivePayments { get; set; }
        public virtual ICollection<SubProject> SubProjects { get; set; }
        public virtual ICollection<VatTax> VatTaxes { get; set; }
        public virtual ICollection<BGBFund> BGBFunds { get; set; }
        #endregion
    }
}
