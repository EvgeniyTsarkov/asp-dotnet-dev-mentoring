using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthwindWebsite.Presentation.HtmlHelpers;

[HtmlTargetElement("northwind-image", TagStructure = TagStructure.NormalOrSelfClosing)]
public class NorthwindImageTagHelper : TagHelper
{
    public int Id { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "a";

        var uploadImageLink = "/Categories/ImageUpload/" + Id;

        output.Attributes.SetAttribute("href", uploadImageLink);
    }
}
