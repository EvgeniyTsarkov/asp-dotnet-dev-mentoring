using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindWebsite.Presentation.HtmlHelpers;

public static class NorthwindImageExtensions
{
    public static HtmlString NorthwindImageLink(this IHtmlHelper helper, int id, int width, double height, string imageType)
    {
        string code = string.Format(
            "<img src=\"/Images/{0}\" width=\"{1}\", height=\"{2}\" type=\"{3}\"/>",
            id, width, height, imageType);

        return new HtmlString(code);
    }
}
