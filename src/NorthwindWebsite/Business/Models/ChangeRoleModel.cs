using Microsoft.AspNetCore.Identity;

namespace NorthwindWebsite.Business.Models;

public class ChangeRoleModel
{
    public string UserId { get; set; }

    public string UserEmail { get; set; }

    public IList<IdentityRole> AllRoles { get; set; } = new List<IdentityRole>();

    public IList<string> UserRoles { get; set; } = new List<string>();
}