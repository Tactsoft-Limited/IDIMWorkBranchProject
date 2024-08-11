namespace IDIMWorkBranchProject.Extentions
{
    public static class Messages
    {
        public static Message Success(string type)
        {
            return new Message { Text = $"{type}d successfully.", Type = "success", Icon = "fa-check" };
        }

        public static Message Failed(string type, string error)
        {
            return new Message { Text = $"Failed to {type.ToLower()}. Error : {error}", Type = "danger", Icon = "fa-times" };
        }

        public static Message InvalidInput(string type)
        {
            return new Message { Text = $"Failed to {type.ToLower()}. Please enter valid input.", Type = "danger", Icon = "fa-times" };
        }

    }
}
