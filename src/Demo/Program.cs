using Demo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyLab.BlazorAdmin;
using MyLab.BlazorAdmin.Services.PageNavigation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddAdminServicesDemo();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.Configure<PageNavigationOptions>(opts =>
{
    opts.Navigation = new NavigationCategoryDefinition[]
        {
            new()
            {
                Title = "",
                Nodes = new PageNavigationNodeDefinition[]
                {
                    new() { Title = "Home", FaClass = "home", UrlItem = ""},
                }
            },
            new()
            {
                Title = "Components",
                Nodes = new PageNavigationNodeDefinition[]
                {
                    new() { Title = "Dialogs", FaClass = "comment-alt", UrlItem = "dialogs"},
                    new() { Title = "Alerts", FaClass = "exclamation-triangle", UrlItem = "alerts"},
                    new() { Title = "Description", FaClass = "info-circle", UrlItem = "description"},
                },
            }
        };
});

await builder.Build().RunAsync();
