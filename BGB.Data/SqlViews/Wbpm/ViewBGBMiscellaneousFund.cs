using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewBGBMiscellaneousFund", Schema = "wbpm")]
    public class ViewBGBMiscellaneousFund
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FundId { get; set; }
        public int ProjectWorkId { get; set; }
        public int ADPReceivePaymentId { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public string FirmNameB { get; set; }
        public string LetterNo { get; set; }
        public DateTime DepositeDate { get; set; }
        public string PayOrderNo { get; set; }
        public DateTime PayOrderDate { get; set; }
        public string BankName { get; set; }
        public string BrunchName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }

    }
}
