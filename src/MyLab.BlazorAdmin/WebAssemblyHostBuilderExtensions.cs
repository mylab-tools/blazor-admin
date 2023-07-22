using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLab.BlazorAdmin.Services.PageNavigation;
using Microsoft.Extensions.Options;
using MyLab.BlazorAdmin.Services;
using MyLab.BlazorAdmin.Test;

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
            
            hBuilder.Services
                .Configure<TestOptions>(hBuilder.Configuration.GetSection(TestOptions.SectionName))
                .AddScoped<IUserInfoProvider, DefaultUserInfoProvider>()
                .AddSingleton<IPageNavigator, PageNavigator>()
                .AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"))
                .AddHttpClient<HttpClient>("api", (sp,c) =>
                {
                    var testOpts = sp.GetRequiredService<IOptions<TestOptions>>();
                    c.BaseAddress = new Uri(GetBaseAddr(testOpts));
                })
                .AddHttpMessageHandler(sp =>
                {
                    var testOpts = sp.GetRequiredService<IOptions<TestOptions>>();
                    var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { GetBaseAddr(testOpts) }
                        );
                    return handler;
                });

            string GetBaseAddr(IOptions<TestOptions>? opts) => opts?.Value.BaseApiUrl ?? hBuilder.HostEnvironment.BaseAddress;
            
        }

        /// <summary>
        /// Adds test services when Test:UseIdentity configuration exists
        /// </summary>
        /// <remarks>
        /// Use it method after all service registrations
        /// </remarks>
        public static IServiceCollection TryAddAdminTestServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var useIdentitySection = configuration.GetSection($"{TestOptions.SectionName}:{nameof(TestOptions.UseIdentity)}");

            if (useIdentitySection.Exists() && !string.IsNullOrWhiteSpace(useIdentitySection.Value))
            {
                services
                    .AddScoped<AuthenticationStateProvider>(sp =>
                    {
                        var foundIdentity = GetTestIdentity(sp);
                        return new TestAuthStateProvider(foundIdentity);
                    })
                    .AddScoped<IAccessTokenProvider>(sp =>
                    {
                        var foundIdentity = GetTestIdentity(sp);
                        return new SingleAccessTokenProvider(foundIdentity.Token);
                    })
                    .AddScoped<IUserInfoProvider>(sp =>
                    {
                        var foundIdentity = GetTestIdentity(sp);
                        return new TestUserInfoProvider(foundIdentity);
                    });
            }

            TestIdentity GetTestIdentity(IServiceProvider sp)
            {
                var testOpts = sp.GetRequiredService<IOptions<TestOptions>>().Value;

                if (!testOpts.Identities.TryGetValue(testOpts.UseIdentity, out var foundTestIdentity))
                    throw new InvalidOperationException($"Test identity '{testOpts.UseIdentity}' not found");

                return foundTestIdentity;
            }

            return services;
        }
    }
}
