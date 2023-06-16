using DataAccess.Entities;
using DataTransferObject.Login;
using DataTransferObject.RegisterDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
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
                ApplicationUser user = new ApplicationUser();
                user.Email = registerDto.Email;
                user.UserName = registerDto.Email;
                user.Name = registerDto.Name;
                user.City = registerDto.City;
                user.State = registerDto.State;
                user.StreetAddress = registerDto.StreetAddress;
                user.PostalCode = registerDto.PostalCode;

                IdentityResult result =await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
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
        public IActionResult Login(LoginDto obj)
        {
            if (ModelState.IsValid)
            {

                var result = _signInManager.PasswordSignInAsync
                (obj.Email, obj.Password,
                  obj.RememberMe, false).GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Customer" });
                }

                ModelState.AddModelError("", "Invalid login!");
            }

            return View(obj);
        }


        public IActionResult Logout()
        {
            // Sign out the user
            _signInManager.SignOutAsync().GetAwaiter().GetResult();

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
    }
}