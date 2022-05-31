namespace NorthwindWebsite.Core.Utils
{
    public static class FileUtils
    {
        public static string GetContentType (this IFormFile file) =>
            file.ContentType.Split("/")[1];
    }
}
