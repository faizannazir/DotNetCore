using Business.AccountServices;
using DataTransferObject.Login;
using DataTransferObject.RegisterDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {

        private readonly IAccount accountServices;

        public AccountController(IAccount account)
        {
           accountServices = account;   
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                if (await accountServices.RegisterUserAsync(registerDto))
                {
                    return RedirectToAction("Login");
                }
            }
            return View(registerDto);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginDto obj)
        {
            if (ModelState.IsValid)
            {
                if (await accountServices.LoginUser(obj))
                {
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }

                ModelState.AddModelError("", "Invalid login!");
            }

            return View(obj);
        }

        [Authorize]
        public IActionResult Logout()
        {
            // Sign out the user
            accountServices.LogoutUser();
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}