using ContactsManagerWebAPI.Core.Identity;
using ContactsManagerWebAPI.Core.Services;
using ContactsManagerWebAPI.Core.ServicesContracts;
using ContactsManagerWebAPI.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace ContactsManagerWebAPI
{
    /// <summary>
    /// Classe principale de l'application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Fonction
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationApiDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));

                //Authorization policy
                var policy= new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                
            })
            .AddXmlDataContractSerializerFormatters();

            builder.Services.AddApiVersioning(config =>
            {
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
                config.DefaultApiVersion = new ApiVersion(1,0);
                config.AssumeDefaultVersionWhenUnspecified = true;

            }); 
            
            //Manage versions of APi
            builder.Services.AddEndpointsApiExplorer();  // Generate description for all endpoints
            builder.Services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() {
                    Title = "City Web",
                    Version = "1.0",
                    Description = "Api for manage cities",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Yves-Régis",
                        Email = "yves.regis@example.com"
                    }
                });
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { 
                    Title = "City Web",
                    Version = "2.0" ,
                    Description = "Api for manage cities",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {                         
                        Name = "Yves-Régis",
                        Email = "yves.regis@example.com"
                    }
                } );
            });
            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            //Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder.WithOrigins(builder.Configuration
                    .GetSection("AllowedOrigins").Get<string[]>())
                    .WithHeaders("Authorization", "Origin", "accept", "content-type")
                    .WithMethods("GET", "POST","PUT","DELETE");
                    }
                );

                options.AddPolicy("4100Client",policyBuilder =>
                {
                    policyBuilder.WithOrigins(builder.Configuration
                    .GetSection("AllowedOrigins2").Get<string[]>())
                    .WithHeaders("Authorization", "Origin", "accept")
                    .WithMethods("GET");
                }
               );

            });
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(
               options =>
               {
                   options.Password.RequiredLength = 6;
                   options.Password.RequireUppercase = true;
                   options.Password.RequireLowercase = true;
                   options.Password.RequireNonAlphanumeric = true;
                   options.Password.RequireDigit = true;
               })
               .AddEntityFrameworkStores<ApplicationApiDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationApiDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationApiDbContext, Guid>>();
            builder.Services.AddTransient<IJwtService, JwtService>();

            //Json web Token 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer( options =>
            {
              
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
                };
            });

            Console.WriteLine($"Audience {builder.Configuration["Jwt:Audience"]}");
            Console.WriteLine($"Issuer {builder.Configuration["Jwt:Issuer"]}");

            builder.Services.AddAuthorization(options => { } );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
           
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();  // create endoints for swagger.json
            app.UseSwaggerUI(options =>  //For Swagger
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json","1.0");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
            });  //Create swagger UI for Testing API / Action Methode            
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
