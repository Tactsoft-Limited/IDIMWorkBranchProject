using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Pm
{
    [Table("pm.ViewProjectList")]
    public partial class ViewProjectList
    {
        public string ProjectName { get; set; }

        public string SubProjectTitle { get; set; }

        public string FiscalYearDescription { get; set; }

        public string UnitName { get; set; }

        public string ConstructionFirmName { get; set; }

        public double AgreementCost { get; set; }

        public DateTime? AgreementDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? HandOverDate { get; set; }

        public string Remark { get; set; }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubProjectId { get; set; }

        public string ProjectStatus { get; set; }

        public string BudgetType { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FiscalYearId { get; set; }
        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConstructionFirmId { get; set; }

        public double BudgetCapital { get; set; }

        public double BudgetRevenue { get; set; }

        public string ProjectCode { get; set; }
    }
}