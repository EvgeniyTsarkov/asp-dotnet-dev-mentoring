using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Core.Utils;
using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Presentation.Controllers;

[Authorize(Roles = RolesConstants.Admin)]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index() => View();

    public IActionResult Users() 
    {
        var users = _userManager.Users.ToList();

        return View(users);
    }
}
