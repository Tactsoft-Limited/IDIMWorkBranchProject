using BGB.Data.Entities.Pm;
using BGB.Data.Entities.Wbpm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Budget
{
    [Table("budget.FiscalYear")]
    public partial class FiscalYear
    {
        public FiscalYear()
        {
            BillPayments = new HashSet<BillPayment>();
            Projects = new HashSet<Project>();
            FinancialYearAllocations = new HashSet<FinancialYearAllocation>();
            FiscalYearExpenses = new HashSet<FiscalYearExpense>();
        }

        public int FiscalYearId { get; set; }

        [Required]
        [StringLength(50)]
        public string FiscalYearDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }



        #region Navigation Properties
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<BillPayment> BillPayments { get; set; }
        public virtual ICollection<FinancialYearAllocation> FinancialYearAllocations { get; set; }
        public virtual ICollection<FiscalYearExpense> FiscalYearExpenses { get; set; }
        #endregion
    }
}
