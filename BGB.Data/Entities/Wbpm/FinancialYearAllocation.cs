using BGB.Data.Entities.Budget;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{

    [Table("FinancialYearAllocation", Schema = "wbpm")]
    public class FinancialYearAllocation : BaseEntity
    {


        [Key]
        public int FinancialYearAllocationId { get; set; }

        public int ADPProjectId { get; set; }
        public int FiscalYearId { get; set; }
        public float TotalAllocation { get; set; }
        public float ADPAllocation { get; set; }
        public float RADPAllocation { get; set; }


        #region Navigation Property
        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }

        [ForeignKey(nameof(FiscalYearId))]
        public virtual FiscalYear FiscalYear { get; set; }
        #endregion
    }
}
