using BGB.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGB.Data.Entities.Wbpm
{
    [Table("RevenueNoha", Schema = "wbpm")]
    public class RevenueNoha:BaseEntity
    {
        [Key]
        public int RevenueNohaId { get; set; }
        public int RevenueId { get; set; }
        public string LetterNo { get; set; }
        public DateTime? RevenueNohaDate { get; set; }
        public string RevenueNohaScanDocument { get; set; }

    }
}
