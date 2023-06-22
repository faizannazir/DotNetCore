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

        public Account(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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


        //public async Task<string> JWTGenerator(ApplicationUser user)
        //{

        //   var jwTokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
        //    var tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim("Id", user.Id),
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
        //        }),
        //        Expires = DateTime.Now.AddHours(1),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256),
        //    };

        //    var token = jwTokenHandler.CreateToken(tokenDescriptor);
        //    var jwtToken = jwTokenHandler.WriteToken(token);
        //    return jwtToken;
        //}
    }
}
