using Crud.Persistance.Features.Membership;
using CVBuilder.Infrastructure.Securities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;



namespace CVBuilder.Persistence.Extentions
{
    public static class IdentityServiceCollectionExtentions 
    {
        public static void AddIdentity(this IServiceCollection services)
        {
            //Microsoft.AspNetCore.Identity.UI need this package
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<ApplicationUserManager>()
                .AddRoleManager<ApplicationRoleManager>()
                .AddSignInManager<ApplicationSignInManager>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                options.SignIn.RequireConfirmedEmail = true;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthorization( options =>
            { 
                options.AddPolicy("CreatePolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Create Claim", "true");
                });
                options.AddPolicy("ViewPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("View Claim", "true");
                });
                options.AddPolicy("SuperAdminPolicy",
                    policy => policy.AddRequirements(new ManageUserViewRequirement()));
            });
            //register custom authorization handler
            services.AddSingleton<IAuthorizationHandler, ManageUserViewRequirementHandler>();

            //Access Dedied page
            services.ConfigureApplicationCookie(option =>
            {
                option.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });


            services.AddRazorPages();
        }
    }
}
