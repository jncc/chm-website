using cloudscribe.Core.Web.Components;
using esdm.shared.ConfigProvider.Resolvers;
using esdm.shared.ConfigProvider.Web.UI.Localisation;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace chm.Website.Config
{
    public class TextPackScopeResolver<T> : IScopeResolver<T> where T : class, new()
    {
        private IScopeIdentifier[] _scopes;
        private readonly string[] _supportedScopeTypes;

        public TextPackScopeResolver(IHttpContextAccessor accessor, ISiteContextResolver siteResolver)
        {
            var candidates = new List<ScopeIdentifier>();

            // Cloudscribe can force the culture from admin UI...
            // this goes through middleware that sets it on the thread.
            // We think that if this has been set away from the default, then this should always 
            // pre-empt any local user choices coming in on the browser headings.
            // (Needs testing how the Tenant resolving below will play out in a multi-tenant scenario) 
            var Tenant = siteResolver.ResolveSite(accessor.HttpContext.Request.Host.Host, accessor.HttpContext.Request.Path);
            if (Tenant?.Result != null && Tenant.Result.ForcedUICulture != null)  // has it been set away from default
            {
                candidates.Add(new ScopeIdentifier("Language", Tenant.Result.ForcedUICulture));
            }

            // Take all accepted languages from the headers in quality order, and match a scope for any of them.
            // (Note: this will match obscure languages down the list if no scoped overrides are defined for the top ones like en-GB.
            // Consider alternative of just using the first one: languages.First().Value; )
            var languages = accessor.HttpContext.Request.Headers["Accept-Language"].ToString().Split(',')
                           .Select(StringWithQualityHeaderValue.Parse)
                           .OrderByDescending(s => s.Quality.GetValueOrDefault(1));

            foreach (var lang in languages)
            {
                candidates.Add(new ScopeIdentifier("Language", lang.Value));
            }

            _scopes = candidates.ToArray();

            _supportedScopeTypes = _scopes.GroupBy(x => x.Type).Select(y => y.First().Type.ToString()).ToArray();
        }

        public IScopeIdentifier[] GetScope() => _scopes;

        public string[] SupportedScopeTypes => _supportedScopeTypes;
    }

    public class SupportedScopeTypesResolver : ISupportedScopeTypesResolver
    {
        
        private IScopeResolver<ConfigurationTextPack> _configTextPackScopeResolver { get; set; }


        public SupportedScopeTypesResolver(IScopeResolver<ConfigurationTextPack> configTextPackScopeResolver)
        {

            _configTextPackScopeResolver = configTextPackScopeResolver;
        }
            


        public Dictionary<string, string[]> SupportedScopes
        {
            get
            {
                return new Dictionary<string, string[]>{
                    { "ConfigurationTextPack", _configTextPackScopeResolver.SupportedScopeTypes }
                    
                };
            }
        }
    }
}
