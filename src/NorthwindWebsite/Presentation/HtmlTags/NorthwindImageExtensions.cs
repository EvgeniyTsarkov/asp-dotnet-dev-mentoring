using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindWebsite.Presentation.HtmlHelpers;

public static class NorthwindImageExtensions
{
    private const string ImageLinkPattern = "<img src=\"/Images/{0}\" width=\"{1}\", height=\"{2}\" type=\"{3}\"/>";

    public static HtmlString NorthwindImageLink(this IHtmlHelper helper, int id, int width, double height, string imageType)
    {
        var code = string.Format(ImageLinkPattern, id, width, height, imageType);

        return new HtmlString(code);
    }
}
