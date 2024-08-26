using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Controllers;

[Authorize]
public class AccountController : Controller
{
  public IActionResult Logout()
  {
    var callbackUrl = Url.Action(nameof(LogoutCallback), "Account", values: null, protocol: Request.Scheme);
    return SignOut(
        new AuthenticationProperties { RedirectUri = callbackUrl },
        OpenIdConnectDefaults.AuthenticationScheme,
        CookieAuthenticationDefaults.AuthenticationScheme);
  }
  public IActionResult LogoutCallback()
  {
    if (User.Identity.IsAuthenticated)
    {
      // Redirect to home page if the user is authenticated.
      return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    return RedirectToAction(nameof(HomeController.Index), "Home");
  }
}
