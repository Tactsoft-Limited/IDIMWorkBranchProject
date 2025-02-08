using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ContractorBillPayment")]
    public class ContractorBillPayment
    {

        [Key]
        public int ContractorBillPaymentId { get; set; }
        public string LetterNo { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal BillAmount { get; set; }
        public string CheckNumber { get; set; }
        public string BankName { get; set; }
        public string BrunchName { get; set; }
        public string AccountNumber { get; set; }
        public string Remarks { get; set; }
        public int CreatedUser { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? UpdatedUser { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public int UpdateNo { get; set; }
    }
}
