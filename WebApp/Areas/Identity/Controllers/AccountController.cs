using Business.AccountServices;
using DataAccess.Entities;
using DataTransferObject.Login;
using DataTransferObject.RegisterDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {

        private readonly IAccount accountServices;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccount account, UserManager<ApplicationUser> userManager)
        {
            accountServices = account;
            _userManager = userManager;
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
                if (await _userManager.FindByEmailAsync(registerDto.Email) == null)
                {
                    if (await accountServices.RegisterUserAsync(registerDto))
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(registerDto.Email), "Email already registered");
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

                ModelState.AddModelError("", "Invalid credentials!");
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