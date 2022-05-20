using Journey.Data;
using Journey.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Journey.Controllers
{
    public class AccountController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        public AccountController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Credential credential)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            var account = unitOfWork.AccountRepo.FindByName(credential.UserName);
            if (account == null)
            {
                return Content("account == null");
            }
            if (credential.Password != account.Password)
            {
                return Content("credential.Password != account.Password");
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, account.AccountName));
            claims.Add(new Claim(ClaimTypes.Role, account.Role.ToString()));
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties();
            properties.IsPersistent = credential.RememberMe;
            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, properties);
            return RedirectToAction("Index", account.Role.ToString());
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}
