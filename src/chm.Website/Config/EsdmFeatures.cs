using chm.Website.Config;
using esdm.shared.ConfigProvider.Resolvers;
using esdm.shared.ConfigProvider.Web.UI.Localisation;
using esdm.shared.RedirectManager;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EsdmFeatures
    {
        public static IServiceCollection AddEsdmServices(this IServiceCollection services, IConfiguration config)
        {

            var connectionString = config.GetConnectionString("EntityFrameworkConnection");

            services.AddConfigurationProviderPostgreSqlStorage(connectionString);
            services.AddConfigurationProviderPostgreSqlDirectDatabaseCommands(config);
            services.AddConfigurationProviderUIServices(config);
           
            services.AddScoped<IScopeResolver<ConfigurationTextPack>, TextPackScopeResolver<ConfigurationTextPack>>();
            services.AddScoped<ISupportedScopeTypesResolver, SupportedScopeTypesResolver>();

            services.AddRedirectManager(config);


            return services;
        }

    }
}
