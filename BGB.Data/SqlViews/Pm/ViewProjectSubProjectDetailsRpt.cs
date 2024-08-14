using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Pm
{
    [Table("pm.ViewProjectSubProjectDetailsRpt")]
    public partial class ViewProjectSubProjectDetailsRpt
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        public string FiscalYearDescription { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuthorizeUnitId { get; set; }

        public string ProjectName { get; set; }

        public string SubProjectTitle { get; set; }

        public string Description { get; set; }

        public string UnitName { get; set; }

        public string ProjectUnit { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FiscalYearId { get; set; }
        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UnitId { get; set; }

        public string ProblemDescription { get; set; }

        public DateTime? IssueDate { get; set; }

        public string Status { get; set; }

        public DateTime? SolveDate { get; set; }

        public int? NoOfDays { get; set; }

        public DateTime? ExtendDate { get; set; }

        public DateTime? NewHandOverDate { get; set; }

        public string Reason { get; set; }

        public string Remark { get; set; }

        public string ConstructionFirmName { get; set; }

        public double AgreementCost { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Subprojectstatus { get; set; }

        public DateTime? HandOverDate { get; set; }

        public string Subprojectremark { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime? ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public string ProjectDirector { get; set; }

        public double BudgetCapital { get; set; }

        public double BudgetRevenue { get; set; }

        public string projectdescription { get; set; }

        public string PicMeetingNo { get; set; }

        public string Projectremark { get; set; }

        public string SourceType { get; set; }

        public string PaymentType { get; set; }

        public double? PaymentAmount { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string AccountName { get; set; }
        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubProjectId { get; set; }

        public string BillTypeName { get; set; }
        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? BillTypeId { get; set; }
        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectStatusId { get; set; }
        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Progress { get; set; }

        public DateTime? StatusDate { get; set; }

        public string StatusDescription { get; set; }

        public string StatusRemark { get; set; }
    }
}