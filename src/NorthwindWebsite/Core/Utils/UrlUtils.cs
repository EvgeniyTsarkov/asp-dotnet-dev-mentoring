namespace NorthwindWebsite.Core.Utils
{
    public static class UrlUtils
    {
        public static string GetImageIndexFromRequest(this string url) =>
            url.Split("/").Last();
    }
}
