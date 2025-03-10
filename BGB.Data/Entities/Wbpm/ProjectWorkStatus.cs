using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("ProjectWorkStatus", Schema = "wbpm")]
    public class ProjectWorkStatus : BaseEntity
    {
        [Key]
        public int ProjectWorkStatusId { get; set; }
        public int ProjectWorkId { get; set; }
        public int? StatusTypeId { get; set; }



        [ForeignKey(nameof(ProjectWorkId))]
        public virtual ProjectWork ProjectWork { get; set; }
    }
}
