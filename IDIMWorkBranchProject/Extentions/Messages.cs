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

        public static Message FileUploadFailed(string type)
        {
            string errorMessage;

            // Custom error messages based on the file type
            switch (type.ToLower())
            {
                case "image":
                    errorMessage = "Image upload failed. Please check the file format and size.";
                    break;
                case "document":
                    errorMessage = "Document upload failed. Please check the file format and size.";
                    break;
                case "audio":
                    errorMessage = "Audio file upload failed. Please check the file format and size.";
                    break;
                case "video":
                    errorMessage = "Video file upload failed. Please check the file format and size.";
                    break;
                default:
                    errorMessage = "File upload failed. Please try again later.";
                    break;
            }

            // Return the message with the appropriate error message
            return new Message
            {
                Text = errorMessage,
                Type = "danger", // Assuming this is a type used to signify error messages
                Icon = "fa-times"
            };
        }


    }
}
