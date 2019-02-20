using esdm.shared.ConfigProvider.Resolvers;
using esdm.shared.ConfigProvider.Web.UI.Localisation;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudscribeFeatures
    {

        public static IServiceCollection SetupDataStorage(
            this IServiceCollection services,
            IConfiguration config,
            bool useHangfire
            )
        {
            var connectionString = config.GetConnectionString("EntityFrameworkConnection");
            
            services.AddCloudscribeCorePostgreSqlStorage(connectionString);
            services.AddCloudscribeLoggingPostgreSqlStorage(connectionString);

            services.AddCloudscribeSimpleContentPostgreSqlStorage(connectionString);

            

            return services;
        }

        public static IServiceCollection SetupCloudscribeFeatures(
            this IServiceCollection services,
            IConfiguration config
            )
        {

            services.AddCloudscribeLogging(config);


            services.AddCloudscribeSimpleContactForm(config);
            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.Web.Navigation.NavigationNodePermissionResolver>();
            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.SimpleContent.Web.Services.PagesNavigationNodePermissionResolver>();
            services.AddCloudscribeCoreMvc(config);
            services.AddCloudscribeCoreIntegrationForSimpleContent(config);
            services.AddSimpleContentMvc(config);
            services.AddContentTemplatesForSimpleContent(config);

            services.AddMetaWeblogForSimpleContent(config.GetSection("MetaWeblogApiOptions"));
            services.AddSimpleContentRssSyndiction();
            

            return services;
        }

    }
}
