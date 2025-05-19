using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using BGB.Data.Entities.Base;

namespace BGB.Data.Entities.Wbpm
{
    [Table("FormalMeeting", Schema = "wbpm")]
    public class FormalMeeting : BaseEntity
    {


        [Key]
        public int FormalMeetingId { get; set; }

        public int ADPProjectId { get; set; }
        public string MeetingTitle { get; set; }
        public int NumberOfMeeting { get; set; }
        public DateTime MeetingDate { get; set; }
        public string MeetingDocument { get; set; }

        #region Navigation Property

        [ForeignKey(nameof(ADPProjectId))]
        public virtual ADPProject ADPProject { get; set; }
        #endregion
    }
}
