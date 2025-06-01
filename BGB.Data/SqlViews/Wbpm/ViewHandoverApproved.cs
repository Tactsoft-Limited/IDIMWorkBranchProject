using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.SqlViews.Wbpm
{
    [Table("ViewHandoverApproved", Schema = "wbpm")]
    public class ViewHandoverApproved
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HandoverApprovedId { get; set; }
        public int ProjectWorkId { get; set; }
        public string LetterNo { get; set; }
        public string QuoateOne { get; set; }
        public string QuoateTwo { get; set; }
        public DateTime? QuoateOneDate { get; set; }
        public DateTime? QuoateTwoDate { get; set; }
        public string Description { get; set; }
        public string ProjectWorkTitleB { get; set; }
        public string AuthorityNameB { get; set; }
        public string DesignationB { get; set; }
        public string HeadAssistant { get; set; }
        public string ConcernedEngineer { get; set; }
        public string SectionIC { get; set; }
        public string BranchClerk { get; set; }

    }
}
