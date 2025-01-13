using BGB.Data.Entities.Budget;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("FiscalYearExpense", Schema = "wbpm")]
    public class FiscalYearExpense : BaseEntity
    {


        [Key]
        public int FiscalYearExpenseId { get; set; }

        public int ADPProjectId { get; set; }
        public int FiscalYearId { get; set; }
        public float TotalExpense { get; set; }


        #region  Navigation Property
        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }
        [ForeignKey(nameof(FiscalYearId))]
        public virtual FiscalYear FiscalYear { get; set; }
        #endregion
    }
}
