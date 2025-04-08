using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{
    [Table("FurnitureBillPayment", Schema = "wbpm")]
    public class FurnitureBillPayment:BaseEntity
    {
        [Key]
        public int FurnitureBillPaymentId { get; set; }
        public int ProjectWorkId { get; set; }
        public int ConstractorCompanyId { get; set; }
        public int ChangedConstractorCompanyId { get; set; }
        public string LetterNo { get; set; }
        public string QuoteOne { get; set; }
        public string QuoteTwo { get; set; }
        public string QuoteThree { get; set; }
        public string QuoteFour { get; set; }
        public decimal AllocationToFurniture { get; set; }
        public string AllocationToFurnitureInWordB { get; set; }
        public decimal DepositsInFund { get; set; }
        public decimal DepositedInFund { get; set; }
        public string DepositedInFundInWordB { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentAmountInWordB { get; set; }

        
    }
}
