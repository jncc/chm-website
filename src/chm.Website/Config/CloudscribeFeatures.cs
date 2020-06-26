using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using cloudscribe.Core.Storage.EFCore.Common;
using cloudscribe.Core.Storage.EFCore.PostgreSql;
using Microsoft.EntityFrameworkCore;
using cloudscribe.Core.Models;
using cloudscribe.SimpleContent.Storage.EFCore.PostgreSql;
using cloudscribe.SimpleContent.Storage.EFCore.Common;
using cloudscribe.SimpleContent.Models;
using cloudscribe.Logging.EFCore.PostgreSql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using cloudscribe.Logging.Models;
using cloudscribe.Logging;
using cloudscribe.Logging.EFCore;
using cloudscribe.Versioning;
using cloudscribe.Logging.EFCore.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CloudscribeFeatures
    {

        //public static IServiceCollection SetupDataStorage(
        //    this IServiceCollection services,
        //    IConfiguration config,
        //    IWebHostEnvironment env
        //    )
        //{
        //    var connectionString = config.GetConnectionString("EntityFrameworkConnection");


        //    services.AddCloudscribeCorePostgreSqlStorage(connectionString);


        //    services.AddCloudscribeLoggingPostgreSqlStorage(connectionString);

        //    services.AddCloudscribeSimpleContentPostgreSqlStorage(connectionString);

        //    return services;
        //}
        public static IServiceCollection SetupDataStorage(this IServiceCollection services,
                                                                       IConfiguration config)
        {
            var connectionString = config.GetConnectionString("EntityFrameworkConnection");

            //******  DLL compatibility error when involking this from cs.Core:  
            //******  services.AddCloudscribeCorePostgreSqlStorage(connectionString);
            //******  services.AddCloudscribeSimpleContentPostgreSqlStorage(connectionString);
            //******  Workarounds:
            services.AddCloudscribeCoreEFCommon(false);

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<CoreDbContext>(options =>
                    options.UseNpgsql(connectionString,
                    npgsqlOptionsAction: sqlOptions =>
                    {
                    }),
                    optionsLifetime: ServiceLifetime.Singleton
                    );

            services.AddScoped<ICoreDbContext, CoreDbContext>();
            services.AddScoped<IDataPlatformInfo, DataPlatformInfo>();
            services.AddSingleton<ICoreDbContextFactory, CoreDbContextFactory>();

            ///
            services.AddEntityFrameworkNpgsql()
               .AddDbContext<SimpleContentDbContext>(options =>
                   options.UseNpgsql(connectionString,
                   npgsqlOptionsAction: sqlOptions =>
                   {
                   }),
                   optionsLifetime: ServiceLifetime.Singleton
                   );

            services.AddSingleton<ISimpleContentDbContextFactory, SimpleContentDbContextFactory>();
            services.AddScoped<ISimpleContentDbContext, SimpleContentDbContext>();
            services.AddCloudscribeSimpleContentEFStorageCommon();
            services.AddScoped<IStorageInfo, StorageInfo>();

            //
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<LoggingDbContext>(options =>
                    options.UseNpgsql(connectionString,
                    npgsqlOptionsAction: sqlOptions =>
                    {
                        //if (maxConnectionRetryCount > 0)
                        //{
                        //    //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        //    sqlOptions.EnableRetryOnFailure(
                        //        maxRetryCount: maxConnectionRetryCount,
                        //        maxRetryDelay: TimeSpan.FromSeconds(maxConnectionRetryDelaySeconds),
                        //        errorCodesToAdd: transientErrorCodesToAdd);
                        //}


                    }),
                    optionsLifetime: ServiceLifetime.Singleton
                    );



            services.TryAddScoped<IWebRequestInfoProvider, NoopWebRequestInfoProvider>();
            services.AddCloudscribeLoggingEFCommon();
            services.AddScoped<ILoggingDbContext, LoggingDbContext>();
            services.AddSingleton<ILoggingDbContextFactory, LoggingDbContextFactory>();
            services.AddScoped<IVersionProvider, VersionProvider>();
            services.AddScoped<ITruncateLog, Truncator>();
            //


            // End Workarounds                        


            return services;
        }
        public static IServiceCollection SetupCloudscribeFeatures(
            this IServiceCollection services,
            IConfiguration config
            )
        {

            services.AddCloudscribeLogging(config);



            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.Web.Navigation.NavigationNodePermissionResolver>();
            services.AddScoped<cloudscribe.Web.Navigation.INavigationNodePermissionResolver, cloudscribe.SimpleContent.Web.Services.PagesNavigationNodePermissionResolver>();
            services.AddCloudscribeCoreMvc(config);
            services.AddCloudscribeCoreIntegrationForSimpleContent(config);
            services.AddSimpleContentMvc(config);
            services.AddContentTemplatesForSimpleContent(config);

            services.AddMetaWeblogForSimpleContent(config.GetSection("MetaWeblogApiOptions"));
            services.AddSimpleContentRssSyndiction();
            services.AddCloudscribeSimpleContactFormCoreIntegration(config);
            services.AddCloudscribeSimpleContactForm(config);










            return services;
        }

    }
}
