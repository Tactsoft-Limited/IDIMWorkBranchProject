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
    [Table("BGBFund", Schema = "wbpm")]
    public class BGBFund : BaseEntity
    {
        [Key]
        public int BGBFundId { get; set; }
        public int ProjectWorkId { get; set; }
        public int ConstructionCompanyId { get; set; }
        public decimal? AmountDeposited { get; set; }
        public int? PaidFromProjectId { get; set; }
        public decimal? PaidAmount { get; set; }


    }
}
