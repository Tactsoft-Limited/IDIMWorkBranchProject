namespace IDIMWorkBranchProject.Models.User
{
    public class ChangePasswordVm
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ReNewPassword { get; set; }
    }
}