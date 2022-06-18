using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NorthwindWebsite.Presentation.Views.Shared.Components.Breadcrumps;

public class Breadcrumbs : ViewComponent
{
    private const string NavigationStartPoint = "Home";
    private const string HandleActionName = "Handle";
    private const string ImageUploadActionName = "ImageUpload";

    public IViewComponentResult Invoke()
    {
        var path = HttpContext.Request.Path.ToString();

        var breadcrumps = new StringBuilder(NavigationStartPoint);

        var processedPath = ProcessSpecialCases(path);

        breadcrumps.Append(processedPath.Replace("/", " > "));

        return View("_Breadcrumbs", breadcrumps);
    }

    private static StringBuilder ProcessSpecialCases(string path)
    {
        var processedPath = new StringBuilder(string.Empty);

        if (path.Contains(HandleActionName))
        {
            if (path.EndsWith(HandleActionName))
            {
                processedPath.Append(path.Replace(HandleActionName, "Create New"));
            }
            else
            {
                var pathElements = path.Split(HandleActionName);
                processedPath.Append(pathElements[0] + "Edit");
            }
        }
        else if (path.Contains(ImageUploadActionName))
        {
            var pathElements = path.Split(ImageUploadActionName);
            processedPath.Append(pathElements[0] + "Image Upload");
        }
        else
        {
            processedPath.Append(path);
        }

        if (path.Contains(NavigationStartPoint))
        {
            processedPath.Replace("Home/", string.Empty);
        }

        return processedPath;
    }
}
