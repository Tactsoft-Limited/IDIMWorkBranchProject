using System;

namespace IDIMWorkBranchProject.Models.User
{
    public class ActivityLogVm
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? ApplicationId { get; set; }

        public int? UserRoleId { get; set; }

        public string SessionId { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Url { get; set; }

        public string RequestType { get; set; }

        public string ActivityData { get; set; }

        public string Agent { get; set; }

        public string Browser { get; set; }

        public DateTime? ActivityTime { get; set; }

        public string DeviceName { get; set; }

        public string DeviceMac { get; set; }
    }
}