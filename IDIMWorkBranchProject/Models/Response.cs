namespace IDIMWorkBranchProject.Models
{
    public class Response
    {
        public string Message { get; set; }
        public ResponseType Type { get; set; }
    }
    public enum ResponseType
    {
        Success = 1,
        Info = 2,
        Warning = 3,
        Error = 4
    }
}