using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Authorization
    {
        public static AuthorizationOptions SetupAuthorizationPolicies(this AuthorizationOptions options)
        {
            //https://docs.asp.net/en/latest/security/authorization/policies.html


            options.AddCloudscribeLoggingDefaultPolicy();

            options.AddCloudscribeCoreDefaultPolicies();

            options.AddCloudscribeCoreSimpleContentIntegrationDefaultPolicies();


            options.AddPolicy(
                "FileManagerPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            options.AddPolicy(
                "FileUploadPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            options.AddPolicy(
                "FileManagerDeletePolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });


            // add other policies here 

            options.AddPolicy(
                "ConfigurationManagementPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators", "Content Administrators");
                });

            // probably best and recommended to not let this policy be managed from the UI
            // since this policy controls who can manage policies from the UI
            options.AddPolicy(
                "PolicyManagementPolicy",
                authBuilder =>
                {
                    authBuilder.RequireRole("Administrators");
                });

            return options;
        }

    }
}
