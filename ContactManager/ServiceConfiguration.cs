using ContactsManager.Core.Domain.IdentityEntities;
using ContactsManager.Core.RepositoryContract;
using ContactsManager.Core.Services;
using ContactsManager.Core.ServicesContract;
using ContactsManager.Infrastructure.MyDbContext;
using ContactsManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ContactsManager.UI
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Configuration de Serilog
            builder.Host.UseSerilog((context, services, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration)
                      .ReadFrom.Services(services);
            });

            // Ajout des services
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            // Configuration de la base de données
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Injection des dépendances
            builder.Services.AddScoped<ICountryRepository, CountriesRepository>();
            builder.Services.AddScoped<IPersonRepository, PersonsRepository>();
            builder.Services.AddScoped<IPersonService, PersonsService>();
            builder.Services.AddScoped<IcountryService, CountryService>();

            // Ajout d'Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            //Required authenticated User
            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("NotAuthorized",policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return !context.User.Identity!.IsAuthenticated;
                    });
                });
            });

            //If Any User is logged we redirect to login form
            builder.Services.ConfigureApplicationCookie( options =>
            {
                options.LoginPath = "/Account/Login";
            });
               
        }
    }
}
