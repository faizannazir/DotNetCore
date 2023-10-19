using Business.AccountServices;
using Business.CategoryServices;
using Business.ProductServices;
using Business.RoleServices;
using Common;
using DataAccess.AppDbContext;
using DataAccess.Entities;
using DataAccess.GenericRepository;
using DataAccess.Repositories.CategoryRepository;
using DataAccess.Repositories.ProductRepository;
using DataTransferObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApp
{
    public static class RegisterServices
    {
        public static void ConfigureApplicationServices(this WebApplicationBuilder webAppBuilder)
        {
            webAppBuilder.Services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(webAppBuilder.Configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
            webAppBuilder.Services.AddIdentity<ApplicationUser, ApplicationRoles>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            webAppBuilder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            webAppBuilder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            webAppBuilder.Services.AddScoped<IProductRepository, ProductRepository>();


            //webAppBuilder.Services.AddScoped<ICategoryServices, CategoryServices>();
            webAppBuilder.Services.AddScoped<ICategoryServices,CategoryServices>();
            webAppBuilder.Services.AddScoped<IProductServices, ProductServices>();
            webAppBuilder.Services.AddScoped<IAccount, Account>();
            webAppBuilder.Services.AddScoped<IRoleServices, RoleServices>();

            webAppBuilder.Services.AddTransient<IEmailSender, EmailSender>();
            webAppBuilder.Services.AddHttpContextAccessor();
  
            webAppBuilder.Services.AddHttpClient();

            // JWT configuration 
            //         // dotnet user-jwts create
          //  webAppBuilder.Services.Configure<JWTConfig>(webAppBuilder.Configuration.GetSection("JWTConfig"));
          //  webAppBuilder.Services.AddAuthentication(
          //  options =>
          //  {
          //      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          //      options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
          //      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          //  }).AddJwtBearer(
          //      jwt =>
          //      {
          //          var key = Encoding.ASCII.GetBytes(webAppBuilder.Configuration.GetSection("JWTConfig:SecretKey").Value);

          //          jwt.SaveToken = true;

          //          jwt.TokenValidationParameters = new TokenValidationParameters()
          //          {
          //              ValidateIssuerSigningKey = true,
          //              IssuerSigningKey = new SymmetricSecurityKey(key),
          //              ValidateIssuer = true,   // For dev
          //              ValidateAudience = true,
          //              RequireExpirationTime = false,
          //              ValidateLifetime = true
          //          };
          //      });
          //webAppBuilder.Services.AddAuthorization();
            // Email Settings
            webAppBuilder.Services.Configure<EmailSettings>(webAppBuilder.Configuration.GetSection("EmailSettings"));
        }
    }
}
