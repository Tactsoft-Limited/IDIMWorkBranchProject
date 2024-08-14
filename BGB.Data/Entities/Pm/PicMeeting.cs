using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Pm
{
    [Table("pm.PicMeeting")]
    public partial class PicMeeting
    {
        public int PicMeetingId { get; set; }

        [StringLength(255)]
        public string PicMeetingTitle { get; set; }

        public DateTime PicMeetingDate { get; set; }

        public int ProjectId { get; set; }

        public string PicMeetingDescription { get; set; }

        public string PscMeetingDescription { get; set; }

        public string Remark { get; set; }

        public int CreatedUser { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? UpdatedUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        public int UpdateNo { get; set; }

        public virtual Project Project { get; set; }
    }
}
