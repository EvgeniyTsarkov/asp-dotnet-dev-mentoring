// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using NorthwindWebsite.Core.Utils;
using NorthwindWebsite.Infrastructure.Entities;

namespace IdentityExample.Areas.Identity.Pages.Account;

public class ForgotPasswordModel : PageModel
{
    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);

        var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

        if (user == null || !isEmailConfirmed)
        {
            return RedirectToPage("./ForgotPasswordConfirmation");
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        code = StringUtils.EncodeCode(code);
        var callbackUrl = Url.Page(
            "/Account/ResetPassword",
            pageHandler: null,
            values: new { area = "Identity", code },
            protocol: Request.Scheme);

        await _emailSender.SendEmailAsync(
            Input.Email,
            "Reset Password",
            $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        return RedirectToPage("./ForgotPasswordConfirmation");
    }
}
