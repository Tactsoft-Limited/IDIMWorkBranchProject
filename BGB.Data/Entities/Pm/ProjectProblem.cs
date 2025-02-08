using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.ProjectProblem")]
    public partial class ProjectProblem
    {
        public int ProjectProblemId { get; set; }

        public int SubProjectId { get; set; }

        public string Description { get; set; }

        public DateTime? IssueDate { get; set; }

        public bool Status { get; set; }

        public DateTime? SolveDate { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual SubProject SubProject { get; set; }
    }
}
