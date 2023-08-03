using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLab.BlazorAdmin.Services.PageNavigation;
using Microsoft.Extensions.Options;
using MyLab.BlazorAdmin.Services;
using MyLab.BlazorAdmin.Test;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using MyLab.BlazorAdmin.Services.Dialogs;

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
                .AddScoped<DialogService>()
                .AddScoped<IDialogService>(sp => sp.GetRequiredService<DialogService>())
                .AddScoped<IDialogPlaceRegistrar>(sp => sp.GetRequiredService<DialogService>())
                .AddSingleton<IPageNavigator, PageNavigator>()
                .AddScoped(sp =>
                {
                    var httpClient = sp.GetRequiredService<IHttpClientFactory>()
                        .CreateClient("backend");

                    return httpClient;
                })
                .AddScoped<IBackendAddrProvider>(sp =>
                {
                    var testOpts = sp.GetRequiredService<IOptions<TestOptions>>();
                    return new BackendAddrProvider(GetBaseAddr(testOpts));
                })
                .AddHttpClient("backend", (sp, cl) =>
                {
                    var testOpts = sp.GetRequiredService<IOptions<TestOptions>>();
                    cl.BaseAddress = new Uri(GetBaseAddr(testOpts));
                })
                .AddHttpMessageHandler(sp =>
                {
                    var testOpts = sp.GetRequiredService<IOptions<TestOptions>>();
                    var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
                        .ConfigureHandler(
                            authorizedUrls: new[] { GetBaseAddr(testOpts) }
                        );
                    return handler;
                })
                ;

            var useIdentitySection = hBuilder.Configuration.GetSection($"{TestOptions.SectionName}:{nameof(TestOptions.UseIdentity)}");

            if (useIdentitySection.Exists() && !string.IsNullOrWhiteSpace(useIdentitySection.Value))
            {
                hBuilder.Services
                    .AddScoped<AuthenticationStateProvider>(sp =>
                    {
                        var foundIdentity = GetTestIdentity(sp);
                        var navigationManager = sp.GetRequiredService<NavigationManager>();
                        return new TestAuthStateProvider(foundIdentity, navigationManager);
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

            string GetBaseAddr(IOptions<TestOptions>? testOpts) => testOpts?.Value.BaseApiUrl ?? hBuilder.HostEnvironment.BaseAddress;
            
        }

        /// <summary>
        /// Adds admin services without authentication
        /// </summary>
        internal static void AddAdminServicesDemo(this WebAssemblyHostBuilder hBuilder)
        {
            hBuilder.Services
                .Configure<TestOptions>(hBuilder.Configuration.GetSection(TestOptions.SectionName))
                .AddScoped<IUserInfoProvider, DefaultUserInfoProvider>()
                .AddSingleton<IPageNavigator, PageNavigator>()
                .AddScoped<DialogService>()
                .AddScoped<IDialogService>(sp => sp.GetRequiredService<DialogService>())
                .AddScoped<IDialogPlaceRegistrar>(sp => sp.GetRequiredService<DialogService>())
                .AddScoped<AuthenticationStateProvider>(sp =>
                {
                    var foundIdentity = GetTestIdentity(sp);
                    var navigationManager = sp.GetRequiredService<NavigationManager>();
                    return new TestAuthStateProvider(foundIdentity, navigationManager);
                });
        }

        static TestIdentity GetTestIdentity(IServiceProvider sp)
        {
            var testOpts = sp.GetRequiredService<IOptions<TestOptions>>().Value;

            if (!testOpts.Identities.TryGetValue(testOpts.UseIdentity, out var foundTestIdentity))
                throw new InvalidOperationException($"Test identity '{testOpts.UseIdentity}' not found");

            return foundTestIdentity;
        }
    }
}
