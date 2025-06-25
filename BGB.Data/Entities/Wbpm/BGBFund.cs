using BGB.Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("BGBFund", Schema = "wbpm")]
    public class BGBFund : BaseEntity
    {
        [Key]
        public int BGBFundId { get; set; }
        public int ProjectWorkId { get; set; }
        public decimal? AmountDeposited { get; set; }
        public int? PaidFromBGBFundId { get; set; }
        public decimal? PaidAmount { get; set; }
        
    }
}
