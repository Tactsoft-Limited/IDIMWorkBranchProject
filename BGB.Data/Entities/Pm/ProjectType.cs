using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ProjectType")]
    public class ProjectType
    {
        public ProjectType()
        {
            Projects = new HashSet<Project>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectTypeId { get; set; }

        public string ProjectTypeName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
