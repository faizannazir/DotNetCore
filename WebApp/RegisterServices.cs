using Business.AccountServices;
using Business.CategoryServices;
using Business.ProductServices;
using Business.RoleServices;
using Common;
using DataAccess.GenericRepository;
using DataAccess.Repositories.CategoryRepository;
using DataAccess.Repositories.ProductRepository;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApp
{
    public static class RegisterServices
    {
        public static void ConfigureApplicationServices(this WebApplicationBuilder webAppBuilder)
        {
            webAppBuilder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            webAppBuilder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            webAppBuilder.Services.AddScoped<IProductRepository, ProductRepository>();


            //webAppBuilder.Services.AddScoped<ICategoryServices, CategoryServices>();
            webAppBuilder.Services.AddScoped<ICategoryServices,CategoryServices>();
            webAppBuilder.Services.AddScoped<IProductServices, ProductServices>();
            webAppBuilder.Services.AddScoped<IAccount, Account>();
            webAppBuilder.Services.AddScoped<IRoleServices, RoleServices>();

            webAppBuilder.Services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
