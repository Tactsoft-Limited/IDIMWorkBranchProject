using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewFurnitureBillPayment", Schema = "wbpm")]
    public class ViewFurnitureBillPayment
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FurnitureBillPaymentId { get; set; }
        public int ProjectWorkId { get; set; }
        public string ProjectWorkTitleB { get; set; }

        public int ConstractorCompanyId { get; set; }
        public string ConstractorCompanyName { get; set; }

        public int? ChangedConstractorCompanyId { get; set; }
        public string ChangedConstractorCompanyName { get; set; }

        public string LetterNo { get; set; }

        public string QuoteOne { get; set; }
        public string QuoteTwo { get; set; }
        public string QuoteThree { get; set; }
        public string QuoteFour { get; set; }
        public DateTime? QuoteOneDate { get; set; }
        public DateTime? QuoteTwoDate { get; set; }
        public DateTime? QuoteThreeDate { get; set; }
        public DateTime? QuoteFourDate { get; set; }


        public decimal? AllocationToFurniture { get; set; }
        public string AllocationToFurnitureInWordB { get; set; }

        public decimal? DepositsInFund { get; set; }
        public decimal? DepositedInFund { get; set; }
        public string DepositedInFundInWordB { get; set; }

        public decimal? PaymentAmount { get; set; }
        public string PaymentAmountInWordB { get; set; }
        public string HeadAssistant { get; set; }
        public string ConcernedEngineer { get; set; }
        public string SectionIC { get; set; }
        public string BranchClerk { get; set; }
    }
}
