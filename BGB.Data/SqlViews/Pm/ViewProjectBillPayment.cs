using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Pm
{
    [Table("pm.ViewProjectBillPayment")]
    public partial class ViewProjectBillPayment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillPaymentId { get; set; }

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
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubProjectId { get; set; }

        public string ProjectStatus { get; set; }

        public string BudgetType { get; set; }
        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FiscalYearId { get; set; }

        public double BudgetCapital { get; set; }

        public double BudgetRevenue { get; set; }

        public int ConstructionFirmId { get; set; }
        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BillTypeId { get; set; }

        public string SourceType { get; set; }

        public string PaymentType { get; set; }

        public double PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string BillTypeName { get; set; }
    }
}