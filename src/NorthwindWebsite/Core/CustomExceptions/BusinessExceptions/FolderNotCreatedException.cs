namespace NorthwindWebsite.Core.CustomExceptions.BusinessExceptions
{
    public class FolderNotCreatedException : Exception
    {
        public FolderNotCreatedException(string message) : base(message)
        {
        }
    }
}
