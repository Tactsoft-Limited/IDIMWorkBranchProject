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
    [Table("CurrentBillCheck", Schema = "wbpm")]
    public class CurrentBillCheck:BaseEntity
    {
        [Key]
        public int CurrentBillCheckId { get; set; }
        public int? ProjectWorkId { get; set; }
        public int? ContractorCompanyPaymentId { get; set; }
        public string LetterNo { get; set; }
        public DateTime CheckDate { get; set; }
        public string CheckDateB { get; set; }
        public DateTime NoteSheetDate { get; set; }
        public int? OfficerId { get; set; }
    }
}
