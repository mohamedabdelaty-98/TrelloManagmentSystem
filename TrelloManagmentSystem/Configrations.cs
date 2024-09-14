using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TrelloManagmentSystem.Data;
using TrelloManagmentSystem.Models;

namespace TrelloManagmentSystem
{
    public static class Configrations
    {
        public static WebApplicationBuilder AutoFacConfigration(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containrbuilder =>
            {
                containrbuilder.RegisterModule(new AutoFacModule(builder.Configuration));
            });
            return builder;
        }
        public static WebApplicationBuilder AddServicesDependencies(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.AddIdentityServicesConfig(configuration);
            builder.AddFluentValidationConfigration();
            builder.AddSwaggerDocumentationServices();
            return builder;
        }
        private static WebApplicationBuilder AddSwaggerDocumentationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(C =>
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
            return builder;



        }


        private static WebApplicationBuilder AddFluentValidationConfigration(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation()
                        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return builder;
        }


        private static WebApplicationBuilder AddIdentityServicesConfig(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            //services.AddScoped<IAuthService, AuthServices>();
            //services.AddSingleton<IJwtProvider, JwtProvider>();


            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();





            builder.Services.AddAuthentication(option =>
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

            return builder;
        }
    }
}
