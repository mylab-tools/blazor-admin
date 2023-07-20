using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MyLab.BlazorAdmin.ComponentModel;
using MyLab.BlazorAdmin.Services.PageNavigation;

namespace MyLab.BlazorAdmin.Shared
{
    /// <summary>
    /// The default admin layout
    /// </summary>
    public partial class AdminLayout : ILayoutPage
    {
        private readonly IPageNavigator _pageNavigator;
        private readonly NavigationManager _navigationManager;

        object? _currentChild;
        NavigationLink[]? _bottomNavPane;

        string? _pageTitle;
        PageNavigation? _navigationPageDescription;

        /// <summary>
        /// Initializes a new instance of <see cref="AdminLayout"/>
        /// </summary>
        public AdminLayout(IPageNavigator pageNavigator, NavigationManager navigationManager)
        {
            _pageNavigator = pageNavigator;
            _navigationManager = navigationManager;
        }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            _navigationManager.LocationChanged += OnLocationChanged;

            UpdateContextData();

            base.OnInitialized();
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            UpdateContextData();
        }

        void UpdateContextData()
        {
            var currentUri = new Uri(_navigationManager.Uri);
            var desc = _pageNavigator.GetPageDescription(currentUri.LocalPath);

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
