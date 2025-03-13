using BGB.Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Wbpm
{
    [Table("Noha", Schema = "wbpm")]
    public class Noha : BaseEntity
    {
        [Key]
        public int NohaId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public DateTime? NohaDate { get; set; }
        public string ScanDocument { get; set; }



        // Navigation Property (Foreign Key Relationships)
        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }
    }

}
