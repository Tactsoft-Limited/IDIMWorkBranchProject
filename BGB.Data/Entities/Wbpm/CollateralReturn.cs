using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{
    [Table("CollateralReturn", Schema = "wbpm")]
    public class CollateralReturn : BaseEntity
    {
        public int CollateralReturnId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public string QuoteOne { get; set; }
        public string QuoteTwo { get; set; }
        public string QuoteThree { get; set; }
        public DateTime? QuoteOneDate { get; set; }
        public DateTime? QuoteTwoDate { get; set; }
        public DateTime? QuoteThreeDate { get; set; }
        public int? HeadAssistantId { get; set; }
        public int? ConcernedEngineerId { get; set; }
        public int? SectionICId { get; set; }
        public int? BranchClerkId { get; set; }
    }
}
