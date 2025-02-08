using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BGB.Data.Entities.Admin
{
    [Table("admin.ActivityLogs")]
    public partial class ActivityLog
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int ApplicationId { get; set; }

        public int? UserRoleId { get; set; }

        [StringLength(100)]
        public string SessionId { get; set; }

        [StringLength(200)]
        public string Controller { get; set; }

        [StringLength(200)]
        public string Action { get; set; }

        [StringLength(500)]
        public string Url { get; set; }

        [StringLength(50)]
        public string RequestType { get; set; }

        public string ActivityData { get; set; }

        public string Agent { get; set; }

        [StringLength(100)]
        public string Browser { get; set; }

        public DateTime? ActivityTime { get; set; }

        [StringLength(200)]
        public string DeviceName { get; set; }

        [StringLength(500)]
        public string DeviceMac { get; set; }

        public virtual Application Application { get; set; }

        public virtual User User { get; set; }
    }
}
