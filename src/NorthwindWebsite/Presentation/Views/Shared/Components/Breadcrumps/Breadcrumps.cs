using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace NorthwindWebsite.Presentation.Views.Shared.Components.Breadcrumps;

public class Breadcrumps : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var path = HttpContext.Request.Path.ToString();

        var breadcrumps = new StringBuilder("Home");

        var processedPath = ProcessSpecialCases(path);

        breadcrumps.Append(processedPath.Replace("/", " > "));

        return View("_Breadcrumps", breadcrumps);
    }

    private static StringBuilder ProcessSpecialCases(string path)
    {
        var processedPath = new StringBuilder(string.Empty);

        if (path.Contains("Handle"))
        {
            if (path.EndsWith("Handle"))
            {
                processedPath.Append(path.Replace("Handle", "Create New"));
            }
            else
            {
                var pathElements = path.Split("Handle");
                processedPath.Append(pathElements[0] + "Edit");
            }
        }
        else if (path.Contains("ImageUpload"))
        {
            var pathElements = path.Split("ImageUpload");
            processedPath.Append(pathElements[0] + "Image Upload");
        }
        else
        {
            processedPath.Append(path);
        }


        if (path.Contains("Home"))
        {
            processedPath.Replace("Home/", string.Empty);
        }

        return processedPath;
    }
}
