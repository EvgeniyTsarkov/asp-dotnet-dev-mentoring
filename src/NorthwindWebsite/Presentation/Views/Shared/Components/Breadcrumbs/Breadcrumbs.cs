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

        var processedArray = ProcessSpecialCases(path);

        var readyBreadcrumbs = string.Join(" > ", processedArray);

        var breadcrembs = string.Concat(NavigationStartPoint, readyBreadcrumbs);

        return View("_Breadcrumbs", breadcrembs);
    }

    private static string[] ProcessSpecialCases(string path)
    {
        var pathAsArray = path.Split('/');

        for (var i = 0; i < pathAsArray.Length; i++)
        {
            if (pathAsArray[i] == HandleActionName)
            {
                if (pathAsArray.Last() == HandleActionName)
                {
                    pathAsArray[i] = "Create New";
                }
                else
                {
                    pathAsArray[i] = "Edit";
                    var lastElementValue = pathAsArray.Last();
                    pathAsArray = pathAsArray.Where(item => item != lastElementValue).ToArray();
                }
            }

            if (pathAsArray[i] == ImageUploadActionName)
            {
                pathAsArray[i] = "Image Upload";
                var lastElementValue = pathAsArray.Last();
                pathAsArray = pathAsArray.Where(item => item != lastElementValue).ToArray();
            }

            if (pathAsArray[i] == NavigationStartPoint)
            {
                pathAsArray = pathAsArray.Where(item => item != "Home/").ToArray();
            }
        }

        return pathAsArray;
    }
}
