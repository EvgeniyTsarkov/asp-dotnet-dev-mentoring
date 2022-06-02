namespace NorthwindWebsite.Core.CustomExceptions.BusinessExceptions
{
    public class NotAFileException : Exception
    {
        public NotAFileException(string? message) 
            : base(message)
        {
        }
    }
}
