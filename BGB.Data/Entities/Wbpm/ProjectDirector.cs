using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
	[Table("ProjectDirector", Schema = "wbpm")]
    public class ProjectDirector : BaseEntity
    {

        [Key]
        public int ProjectDirectorId { get; set; }
        public int ADPProjectId { get; set; }
        public string ProjectDirectorName { get; set; }
        public string Designation { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public string PDDocument { get; set; }
        public bool IsActive { get; set; }


        #region  Navigation Property
        [ForeignKey(nameof(ADPProjectId))]
        public ADPProject ADPProjects { get; set; }
        #endregion
    }
}
