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
    public partial class AdminLayout : IComponentRegistrar
    {
        object? _currentChild;
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
        /// Gets or sets <see cref="PageNavigationOptions"/>
        /// </summary>
        [Inject]
        public IOptions<PageNavigationOptions>? NavOptions { get; set; }

        /// <summary>
        /// Gets or sets child content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

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

        /// <inheritdoc />
        public IDisposable RegisterChild(object child)
        {
            _currentChild = child;

            if (child is INavigationSource navSource)
            {
                _bottomNavPane = navSource.GetNavigation().ToArray();
                StateHasChanged();
            }

            return new ChildUnregistrar(child, UnregisterChild);
        }

        void UnregisterChild(object child)
        {
            if (_currentChild != child)
                return;

            _bottomNavPane = null;
        }

        class ChildUnregistrar : IDisposable
        {
            private readonly object _child;
            private readonly Action<object> _unregisterAction;

            public ChildUnregistrar(object child, Action<object> unregisterAction)
            {
                _child = child;
                _unregisterAction = unregisterAction;
            }

            public void Dispose()
            {
                _unregisterAction(_child);
            }
        }
    }
}
