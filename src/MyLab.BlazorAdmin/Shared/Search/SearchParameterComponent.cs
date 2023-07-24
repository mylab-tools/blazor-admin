using Microsoft.AspNetCore.Components;
using MyLab.BlazorAdmin.ComponentModel;
using MyLab.BlazorAdmin.Tools.Search;

namespace MyLab.BlazorAdmin.Shared.Search
{
    /// <summary>
    /// the base for search parameters components
    /// </summary>
    public abstract class SearchParameterComponent : ComponentBase, ISearchParameterSource, ISearchStarter, IDisposable
    {
        private IDisposable? _unregistrar;

        /// <summary>
        /// Search page registrar
        /// </summary>
        [CascadingParameter(Name = "SearchPage")]
        protected IComponentRegistrar? SearchPage { get; set; }

        /// <inheritdoc />
        public event EventHandler? SearchStarted;

        /// <inheritdoc />
        public abstract void ApplyParameters(SearchRequest request);

        /// <summary>
        /// Call to safe invoke <see cref="SearchStarted"/>
        /// </summary>
        protected virtual void OnSearchStarted()
        {
            SearchStarted?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        protected override Task OnInitializedAsync()
        {
            _unregistrar = SearchPage?.RegisterChild(this);

            return base.OnInitializedAsync();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _unregistrar?.Dispose();
        }
    }
}
