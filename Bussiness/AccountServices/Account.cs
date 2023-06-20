using DataAccess.Entities;
using DataTransferObject.Login;
using DataTransferObject.RegisterDto;
using Enums;
using Microsoft.AspNetCore.Identity;

namespace Business.AccountServices
{
    public class Account :IAccount
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRoles> _roleManager;

        public Account(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRoles> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> LoginUser(LoginDto loginDto)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, false);
            return result.Succeeded;
        }

        public async Task<bool> LogoutUser()
        {
           await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<bool> RegisterUserAsync(RegisterDto registerDto)
        {

            ApplicationUser user = new ApplicationUser();
            user.Email = registerDto.Email;
            user.UserName = registerDto.Email;
            user.Name = registerDto.Name;
            user.City = registerDto.City;
            user.State = registerDto.State;
            user.StreetAddress = registerDto.StreetAddress;
            user.PostalCode = registerDto.PostalCode;
           
            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user,UserRoles.Role_Customer);
            return result.Succeeded;
        }


    }
}
