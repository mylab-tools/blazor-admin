using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Options;
using MyLab.BlazorAdmin.ComponentModel;
using MyLab.BlazorAdmin.Services.PageNavigation;

namespace MyLab.BlazorAdmin.Shared
{
    /// <summary>
    /// The default admin layout
    /// </summary>
    public partial class AdminLayout
    {
        NavigationLink[]? _bottomNavPane;
        string? _pageTitle;
        PageNavigation? _navigationPageDescription;

        /// <summary>
        /// Specifies header page text
        /// </summary>
        [Parameter]
        public string Header { get; set; } = "MyLab Admin";

        /// <summary>
        /// Specifies copyright text
        /// </summary>
        [Parameter]
        public string Copyright { get; set; } = "Copyright ©";

        /// <summary>
        /// Specifies logo image URL
        /// </summary>
        [Parameter]
        public string LogoUrl { get; set; } = "#";

        /// <summary>
        /// Gets or sets <see cref="NavigationManager"/>
        /// </summary>
        [Inject]
        public NavigationManager? NavigationManager { get; set; }

        /// <summary>
        /// Gets or sets <see cref="IPageNavigator"/>
        /// </summary>
        [Inject]
        public IPageNavigator? PageNavigator { get; set; }

        /// <summary>
        /// Gets or sets child content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Gets or sets fast action icon components
        /// </summary>
        [Parameter]
        public RenderFragment? FastActionIcons { get; set; }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            if (NavigationManager == null)
                throw new InvalidOperationException("NavigationManager is not specified");

            NavigationManager.LocationChanged += OnLocationChanged;

            UpdateContextData();

            base.OnInitialized();
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            UpdateContextData();
        }

        void UpdateContextData()
        {
            if (NavigationManager == null)
                throw new InvalidOperationException("NavigationManager is not specified");
            if (PageNavigator == null)
                throw new InvalidOperationException("PageNavigator is not specified");

            var currentUri = new Uri(NavigationManager.Uri);
            var desc = PageNavigator.GetPageDescription(currentUri.LocalPath);

            if (desc != null)
            {
                _pageTitle = desc.Title;
                _navigationPageDescription = desc;
            }

            StateHasChanged();
        }
    }
}
