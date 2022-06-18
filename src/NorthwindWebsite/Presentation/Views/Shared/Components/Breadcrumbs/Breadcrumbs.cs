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
        var pathAsArray = path.Split('/');

        var pathAsList = new List<string>();

        foreach (var node in pathAsArray)
        {
            if (node == HandleActionName)
            {
                if (pathAsArray.Last() == HandleActionName)
                {
                    pathAsList.Add("Create new");
                    continue;
                }
                else
                {
                    pathAsList.Add("Edit");
                    break;
                }
            }

            if (node == ImageUploadActionName)
            {
                pathAsList.Add("Image Upload");
                break;
            }

            pathAsList.Add(node);
        }

        if (pathAsList.Contains("Home/"))
        {
            pathAsList.Remove("Home/");
        }

        return pathAsList;
    }
}
