using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SurveyBasket.ApI.JwtService;
using System.Reflection;
using System.Text;
using TrelloManagmentSystem.Data;
using TrelloManagmentSystem.Models;
using TrelloManagmentSystem.Services;

namespace TrelloManagmentSystem.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddIdentityServicesConfig(configuration);
            services.AddFluentValidationConfigration();
            services.AddSwaggerDocumentationServices();
            return services;
        }

        private static IServiceCollection AddSwaggerDocumentationServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(C =>
            {
                var SecuritySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorizations",
                    Description = " Jwt Auth Bearer Schema",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,

                    }
                };

                C.AddSecurityDefinition("Bearer", SecuritySchema);
                var ScurityRequirments = new OpenApiSecurityRequirement
                {
                    {
                        SecuritySchema , new [] {"Bearer"}
                    }
                };

                C.AddSecurityRequirement(ScurityRequirments);
            });
            return services;



        }


        private static IServiceCollection AddFluentValidationConfigration(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                             return services;
        }


        private static IServiceCollection AddIdentityServicesConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IAuthService, AuthServices>();
            //services.AddSingleton<IJwtProvider, JwtProvider>();


            services.AddIdentity<AppUser , IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();





            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Token:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Token:ValidAudiance"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });

            return services;
        }

    }
}
