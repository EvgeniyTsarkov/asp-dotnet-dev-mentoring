namespace NorthwindWebsite.Presentation.Utils
{
    public static class UrlUtils
    {
        public static string GetImageIndexFromRequest(string url) =>
            url.Split("/").Last();
    }
}
