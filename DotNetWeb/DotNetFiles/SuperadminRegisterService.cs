using Edu.Business.AccountServices;
using Edu.Business.LicenseCompanyPolicyService;
using Edu.Business.LicenseCompanyServices;
using Edu.Business.LicenseCompanySubscriptionDuplicateService;
using Edu.Business.LicenseCompanySubscriptionService;
using Edu.Business.LicenseCompanySubscriptionTransactionService;
using Edu.Business.LicenseSubscriptionPlanFeatureService;
using Edu.Business.LicenseSubscriptionPlanService;
using Edu.Business.PaymentService;
using Edu.Business.SuperAdminDashBoardService;
using Edu.Common.AzureBlobService;
using Edu.Common.EmailService;
using Edu.Common.Utils;
using Edu.Data.DatabaseInitializer;
using Edu.Data.DbContexts;
using Edu.Data.EFRepository;
using Edu.Data.Repositories.LicenseCompanyPolicy;
using Edu.Data.Repositories.LicenseCompanySubscription;
using Edu.Data.Repositories.LicenseCompanySubscriptionDuplicateHistory;
using Edu.Data.Repositories.LicenseCompanySubscriptionTransaction;
using Edu.Data.Repositories.LicenseEduCompany;
using Edu.Data.Repositories.LicensePaymentConfiguration;
using Edu.Data.Repositories.LicenseSubscriptionFeaturePlan;
using Edu.Data.Repositories.LicenseSubscriptions;
using Edu.Data.Repositories.LicenseUser;
using Edu.Data.Repositories.PortalCoursePrices;
using Edu.Data.Repositories.PortalPaymentConfiguration;
using Edu.Data.Repositories.PortalRole;
using Edu.Data.Repositories.Superadmin;
using Edu.Data.RestClient;
using Edu.Data.TenantHelper;
using Microsoft.EntityFrameworkCore;

namespace Edu.SuperAdmin.RegisterService
{
    public static class SuperadminRegisterService
    {
        public static void ConfigureApplicationServices(this WebApplicationBuilder webAppBuilder)
        {

            webAppBuilder.Services.AddRazorPages();
            webAppBuilder.Services.AddDbContext<EduLicenseDbContext>(options =>
              options.UseSqlServer(
                  webAppBuilder.Configuration.GetConnectionString("DefaultConnection")
                                  ));
            webAppBuilder.Services.AddDbContext<EduPortalDbContext>();
            // configure DI for application services
            webAppBuilder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);
            webAppBuilder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            webAppBuilder.Services.AddScoped<ITenantProvider, TenantProvider>();
            webAppBuilder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            webAppBuilder.Services.AddScoped<IAzureBlobService, AzureBlobService>();
            webAppBuilder.Services.AddScoped<AzureBlobUtility>();

            #region @@@[------Services]
            webAppBuilder.Services.AddScoped<IAccountService, AccountService>();
            webAppBuilder.Services.AddScoped<IEmailService,EmailService>();
            
            webAppBuilder.Services.AddScoped<IPaymentConfigurationService, PaymentConfigurationService>();
            webAppBuilder.Services.AddScoped<ILicenseSubscriptionPlanService,LicenseSubscriptionPlanService>();
            webAppBuilder.Services.AddScoped<ILicenseCompanyService, LicenseCompanyService>();
            webAppBuilder.Services.AddScoped<ILicenseCompanySubscriptionHistoryService, LicenseCompanySubscriptionHistoryService>();
            webAppBuilder.Services.AddScoped<ILicenseUserRepo, LicenseUserRepo>();
            webAppBuilder.Services.AddScoped<ISuperAdminDashBoardService, SuperAdminDashBoardService>();
            webAppBuilder.Services.AddScoped<RestClientService>();
            webAppBuilder.Services.AddScoped<ILicenseCompanyPolicyService, LicenseCompanyPolicyService>();
            webAppBuilder.Services.AddScoped<ILicenseCompanySubscriptionService, LicenseCompanySubscriptionService>();
            webAppBuilder.Services.AddScoped<ILicenseSubscriptionPlanFeatureService, LicenseSubscriptionPlanFeatureService>();
            webAppBuilder.Services.AddScoped<IViewRenderingService, ViewRenderingService>();
            webAppBuilder.Services.AddScoped<ILicenseCompanySubscriptionTransactionService, LicenseCompanySubscriptionTransactionService>();
            #endregion

            #region @@@[------Repository]
            webAppBuilder.Services.AddScoped<ISuperadminRepo, SuperadminRepo>();
            webAppBuilder.Services.AddScoped<ILicensePaymentConfigurationRepo, LicensePaymentConfigurationRepo>();
            webAppBuilder.Services.AddScoped<ILicenseSubscriptionPlanRepo,LicenseSubscriptionPlanRepo>();
            webAppBuilder.Services.AddScoped<ILicenseCompanyRepo, LicenseCompanyRepo>();
            webAppBuilder.Services.AddScoped<ILicenseCompanySubscriptionRepo, LicenseCompanySubscriptionRepo>();
            webAppBuilder.Services.AddScoped<ICompanySubscriptionTransactionRepo, CompanySubscriptionTransactionRepo>();
            webAppBuilder.Services.AddScoped<ILicenseCompanySubscriptionHistoryRepo, LicenseCompanySubscriptionHistoryRepo>();
            webAppBuilder.Services.AddScoped<IPortalRoleRepo, PortalRoleRepo>();
            webAppBuilder.Services.AddScoped<ILicenseCompanyPolicyRepo, LicenseCompanyPolicyRepo>();
            webAppBuilder.Services.AddScoped<ILicenseSubscriptionPlanFeatureRepo, LicenseSubscriptionPlanFeatureRepo>();
            webAppBuilder.Services.AddScoped<IPortalPaymentConfigurationRepo, PortalPaymentConfigurationRepo>();
            webAppBuilder.Services.AddScoped<IPortalCoursePriceRepo, PortalCoursePriceRepo>();

            #endregion
        }
    }
}
