using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("WorkOrder", Schema = "wbpm")]
    public class WorkOrder : BaseEntity
    {
        [Key]
        public int WorkOrderId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public DateTime? WorkOrderDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? HandOverDate { get; set; }
        public string ScanDocument { get; set; }


        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }

    }
}
