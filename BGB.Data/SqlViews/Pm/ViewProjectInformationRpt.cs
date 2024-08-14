using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Pm
{
    [Table("pm.ViewProjectInformationRpt")]
    public partial class ViewProjectInformationRpt
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        public string FiscalYearDescription { get; set; }

        public string UnitName { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime? ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        public string ProjectDirector { get; set; }

        public double BudgetCapital { get; set; }

        public double BudgetRevenue { get; set; }

        public string Description { get; set; }

        public string PicMeetingNo { get; set; }

        public string Remark { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuthorizeUnitId { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FiscalYearId { get; set; }
    }
}