using DataAccess.Entities;
using DataTransferObject.Login;
using DataTransferObject.RegisterDto;
using Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Business.AccountServices
{
    public class Account :IAccount
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly JWTConfig _jwtConfig;
        //private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;

        public Account(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager, 
            //JWTConfig jWTConfig
            //IConfiguration configuration,
            IEmailSender emailSender
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            //_jwtConfig = jWTConfig;
            //this.configuration = configuration;
            this.emailSender = emailSender;
        }

        public async Task<bool> LoginUser(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync( loginDto.Email );
            if ( user == null )
            {
                //return new AuthResult() { Result=false,Token="not a valid user" };
                return false;
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, loginDto.RememberMe, false);
            //if(result.Succeeded)
            //{
            //    await _userManager.GetAuthenticationTokenAsync(user, "", JWTGenerator(user));
            //    var accessResult = new AuthResult() { Result = true , Token = JWTGenerator(user)};
            //    return accessResult;
            //}
            //return new AuthResult() { Result = false,Token = "not a valid user" };
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

            await emailSender.SendEmailAsync(user.Email, "Testing", "This is testing Mail");
            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, UserRoles.Role_Customer);
            //await _userManager.AddToRoleAsync(user,UserRoles.Role_Admin);
            //var token = JWTGenerator(user);

            //var authResult =  new AuthResult()
            //{
            //    Result = true,
            //    Token = token
            //};
            //if (authResult.Result)
            // {
            //     return true;
            // }
            //else
            // {
            //     return false;
            // }
            return result.Succeeded;
        }


        //public string JWTGenerator(ApplicationUser user)
        //{

        //    var jwTokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(configuration.GetSection("JWTConfig:SecretKey").Value);
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
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        //    };

        //    var token = jwTokenHandler.CreateToken(tokenDescriptor);
        //    var jwtToken = jwTokenHandler.WriteToken(token);
        //    return jwtToken;
        //}
    }
}
