using System;

namespace IDIMWorkBranchProject.Extentions.Exceptions
{
    public class DuplicateNameException : Exception
    {
        public DuplicateNameException(string message) : base(string.Format(DefaultMsg.AlreadyExists, message))
        {

        }
    }
}