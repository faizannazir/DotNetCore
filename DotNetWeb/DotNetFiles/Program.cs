using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Edu.SuperAdmin.RegisterService;
using Edu.Data.DatabaseInitializer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Edu.Common.Utils;
using Edu.SuperAdmin.Scheduler;
using Serilog;
using Edu.SuperAdmin.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
       .ReadFrom.Configuration(ctx.Configuration));


// Add services to the container.   
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Login";
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddHostedService<TapBackgroundService>();
builder.Services.AddHostedService<SubscriptionRecurringBackgroundService>();
SuperadminRegisterService.ConfigureApplicationServices(builder);

#region Localization Region

builder.Services.AddMvc().
        AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).
        AddDataAnnotationsLocalization();

builder.Services.AddPortableObjectLocalization(opts => { opts.ResourcesPath = "Resources"; });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedUICultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar-SA"),
            };
    var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
            };

    options.DefaultRequestCulture = new RequestCulture("en-US", "ar-SA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedUICultures;
});
#endregion


var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var DbInitializer = services.GetRequiredService<IDatabaseInitializer>();
    DbInitializer?.MigrateDbsAsync().Wait();
    DbInitializer?.SeedDataAsync().Wait();
    DbInitializer?.SeedPortalDataAsync().Wait();
}
ServiceActivator.Configure(app.Services);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/CustomError");
}
using (var serviceScope = app.Services.CreateScope())
{
    var supportedUICultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar-SA"),
            };
    var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
            };

    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedUICultures
    });

}
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCookiePolicy();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");
app.Run();

