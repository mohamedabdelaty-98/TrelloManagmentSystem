using Autofac;
using Autofac.Extensions.DependencyInjection;

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
    }
}
