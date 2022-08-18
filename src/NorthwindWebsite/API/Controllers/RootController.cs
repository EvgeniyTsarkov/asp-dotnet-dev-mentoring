using Microsoft.AspNetCore.Mvc;

namespace NorthwindWebsite.API.Controllers;

[Route("api")]
[ApiController]
public class RootController : ControllerBase
{
    public ActionResult<object> GetRoot() =>
        new
        {
            products = new { href = Url.Link(nameof(ApiProductsController.GetProducts), null) },
            categories = new { href = Url.Link(nameof(CategoriesController.GetCategories), null) }
        };
}
