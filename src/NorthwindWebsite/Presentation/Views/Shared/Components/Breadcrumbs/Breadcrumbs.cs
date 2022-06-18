using Microsoft.AspNetCore.Mvc;

namespace NorthwindWebsite.Presentation.Views.Shared.Components.Breadcrumps;

public class Breadcrumbs : ViewComponent
{
    private const string NavigationStartPoint = "Home";
    private const string HandleActionName = "Handle";
    private const string ImageUploadActionName = "ImageUpload";

    public IViewComponentResult Invoke()
    {
        var path = HttpContext.Request.Path.ToString();

        var processedPathAsList = ProcessSpecialCases(path);

        var breadcrumbs = NavigationStartPoint + string.Join(" > ", processedPathAsList);

        return View("_Breadcrumbs", breadcrumbs);
    }

    private static List<string> ProcessSpecialCases(string path)
    {
        var originalPathSegments = path.Split('/');

        var breadcrumbPathSegments = new List<string>();

        foreach (var node in originalPathSegments)
        {
            if (node == HandleActionName)
            {
                if (originalPathSegments.Last() == HandleActionName)
                {
                    breadcrumbPathSegments.Add("Create new");
                    continue;
                }
                else
                {
                    breadcrumbPathSegments.Add("Edit");
                    break;
                }
            }

            if (node == ImageUploadActionName)
            {
                breadcrumbPathSegments.Add("Image Upload");
                break;
            }

            breadcrumbPathSegments.Add(node);
        }

        if (breadcrumbPathSegments.Contains(NavigationStartPoint))
        {
            breadcrumbPathSegments.Remove(NavigationStartPoint);
        }

        return breadcrumbPathSegments;
    }
}
