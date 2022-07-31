using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthwindWebsite.Business.Models;
using NorthwindWebsite.Core.Utils;
using NorthwindWebsite.Infrastructure.Entities;

namespace NorthwindWebsite.Presentation.Controllers;

[Authorize(Roles = RolesConstants.Admin)]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public RolesController(
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();

        return View(roles);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(name));

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(name);
    }

    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var allRoles = _roleManager.Roles.ToList();

        var model = new ChangeRoleModel
        {
            UserId = user.Id,
            UserEmail = user.Email,
            UserRoles = userRoles,
            AllRoles = allRoles
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var addedRoles = roles.Except(userRoles);

        var removedRoles = userRoles.Except(roles);

        await _userManager.AddToRolesAsync(user, addedRoles);

        await _userManager.RemoveFromRolesAsync(user, removedRoles);

        return RedirectToAction(nameof(Index));
    }
}
