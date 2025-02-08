using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Pm
{
    [Table("pm.ViewProjectPaymentReceiptRpt")]
    public partial class ViewProjectPaymentReceiptRpt
    {
        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }

        public string UnitName { get; set; }

        public string FiscalYearDescription { get; set; }

        public string QuarterName { get; set; }

        public string DocumentType { get; set; }

        public string SourceType { get; set; }

        public double ReceiptAmount { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public string Remark { get; set; }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AuthorizeUnitId { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FiscalYearId { get; set; }
        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuarterId { get; set; }
    }
}