using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthwindWebsite.Presentation.HtmlHelpers;

[HtmlTargetElement("northwind-image", TagStructure = TagStructure.NormalOrSelfClosing)]
public class NorthwindImageTagHelper : TagHelper
{
    private const string ImageUploadLink = "/Categories/ImageUpload/";

    public int Id { get; set; }

    public string Text { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";

        var uploadImageLink = string.Concat(ImageUploadLink, Id);

        output.Attributes.SetAttribute("href", uploadImageLink);

        if (Text != null)
        {
            output.Attributes.SetAttribute("title", Text);
        }
    }
}
