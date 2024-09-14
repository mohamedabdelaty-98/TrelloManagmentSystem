using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.ApI.JwtService;
using System.Diagnostics;
using TrelloManagmentSystem.Data;
using TrelloManagmentSystem.Repositories.GenericRepositories;
using TrelloManagmentSystem.Services;


namespace TrelloManagmentSystem
{
    public class AutoFacModule : Module
    {
        private readonly IConfiguration configuration;

        public AutoFacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var optionbuilder = new DbContextOptionsBuilder<Context>();
                var ConnectionString = configuration.GetConnectionString("ConnString");
                optionbuilder.UseSqlServer(ConnectionString).LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .EnableSensitiveDataLogging();
                return new Context(optionbuilder.Options);
            }).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof (GenericRepositories<>)).As(typeof(IGenericRepositories<>));


 
			builder.Register(context => new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<Profiles>();
			}).CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
		}
            builder.RegisterType(typeof(AuthService)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType(typeof(JwtProvider)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType(typeof(UserService)).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
