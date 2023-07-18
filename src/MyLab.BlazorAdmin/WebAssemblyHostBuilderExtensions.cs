using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyLab.BlazorAdmin
{
    /// <summary>
    /// Extension methods for <see cref="WebAssemblyHostBuilder"/>
    /// </summary>
    public static class WebAssemblyHostBuilderExtensions
    {
        /// <summary>
        /// OIDC configuration section name
        /// </summary>
        public const string OidcSectionName = "OIDC";

        /// <summary>
        /// Adds admin services
        /// </summary>
        public static void AddAdminServices(this WebAssemblyHostBuilder hBuilder)
        {
            if (hBuilder == null) throw new ArgumentNullException(nameof(hBuilder));
            
            hBuilder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.DefaultScopes.Add("offline_access");
                hBuilder.Configuration.Bind(OidcSectionName, options.ProviderOptions);
            });

            var baseApiAddr = hBuilder.Configuration.GetSection("Test")["BaseApiUrl"] ?? hBuilder.HostEnvironment.BaseAddress;

            hBuilder.Services
                .AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"))
                .AddHttpClient<HttpClient>("api", c => c.BaseAddress = new Uri(baseApiAddr))
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { baseApiAddr }
                        );
                    return handler;
                });
        }
    }
}
