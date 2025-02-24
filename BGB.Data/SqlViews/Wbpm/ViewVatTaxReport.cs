using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewVatTaxReport", Schema = "wbpm")]
    public class ViewVatTaxReport
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ADPReceivePaymentId { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public string FirmNameB { get; set; }
        public int BillNumber { get; set; }
        public double TaxPer { get; set; }
        public decimal TaxAmount { get; set; }
        public double VatPer { get; set; }
        public decimal VatAmount { get; set; }
        public double CollateralPer { get; set; }
        public decimal CollateralAmount { get; set; }
        public decimal BillPaidAmount { get; set; }
        public decimal TotalDeductionAmount { get; set; }
        public string TotalDeductionAmountInWord { get; set; }
    }
}
