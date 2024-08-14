using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Irms;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.WorkBranchStore
{
    [Table("workbranchstore.UnitBudget")]
    public partial class WorkBranchStoreUnitBudget
    {
        [Key]
        public int UnitBudgetId { get; set; }

        [StringLength(255)]
        public string LetterNo { get; set; }

        public DateTime? LetterDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int UnitId { get; set; }

        public int? LocationId { get; set; }

        public int FiscalYearId { get; set; }

        public int? AccountCodeId { get; set; }

        public int? BopId { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual AccountCode AccountCode { get; set; }

        public virtual FiscalYear FiscalYear { get; set; }

        public virtual IrmsSetupLocation SetupLocation1 { get; set; }

        public virtual IrmsSetupUnit IrmsSetupUnit { get; set; }

        public virtual Bop Bop { get; set; }
    }
}
