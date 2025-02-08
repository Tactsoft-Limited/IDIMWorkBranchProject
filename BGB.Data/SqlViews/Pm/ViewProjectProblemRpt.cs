using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BGB.Data.SqlViews.Pm
{
    [Table("pm.ViewProjectProblemRpt")]
    public partial class ViewProjectProblemRpt
    {
        public string UnitName { get; set; }

        public string ProjectName { get; set; }

        public string SubProjectTitle { get; set; }

        public string Description { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? SolveDate { get; set; }

        public bool Status { get; set; }
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubProjectId { get; set; }
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UnitId { get; set; }
        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectId { get; set; }
        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectProblemId { get; set; }
    }
}