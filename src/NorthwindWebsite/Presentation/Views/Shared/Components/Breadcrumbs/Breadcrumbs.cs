using Microsoft.AspNetCore.Mvc;

namespace NorthwindWebsite.Presentation.Views.Shared.Components.Breadcrumps;

public class Breadcrumbs : ViewComponent
{
    private const string NavigationStartPoint = "home";
    private const string HandleActionName = "handle";
    private const string ImageUploadActionName = "imageupload";
    private const string FromApiActionName = "fromapi";


    public IViewComponentResult Invoke()
    {
        var path = HttpContext.Request.Path
            .ToString()
            .ToLower();

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
                    breadcrumbPathSegments.Add("create new");
                    continue;
                }
                else
                {
                    breadcrumbPathSegments.Add("edit");
                    break;
                }
            }

            if (node == ImageUploadActionName)
            {
                breadcrumbPathSegments.Add("image upload");
                break;
            }

            if (node == FromApiActionName) 
            {
                breadcrumbPathSegments.Add("from api");
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
