using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Pm
{
    [Table("pm.ViewProjectWiseSubProjectRpt")]
    public partial class ViewProjectWiseSubProjectRpt
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }

        public string FiscalYearDescription { get; set; }

        public string UnitName { get; set; }

        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public string SubProjectTitle { get; set; }

        public string Description { get; set; }

        public string ConstructionFirmName { get; set; }

        public double AgreementCost { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Status { get; set; }

        public DateTime? HandOverDate { get; set; }

        public string Remark { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConstructionFirmId { get; set; }

        public string SubProjectUnit { get; set; }
    }
}