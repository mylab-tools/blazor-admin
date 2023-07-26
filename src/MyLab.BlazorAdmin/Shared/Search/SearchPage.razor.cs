using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MyLab.BlazorAdmin.ComponentModel;
using MyLab.BlazorAdmin.Tools.Search;
using System.ComponentModel;

namespace MyLab.BlazorAdmin.Shared.Search;

/// <summary>
/// Provides abilities o organize and visualize a searching
/// </summary>
/// <typeparam name="T">Searching result item type</typeparam>
public partial class SearchPage<T> : IDisposable, IComponentRegistrar
{
    private SearchResult<T>? _searchResult;
    private SearchRequest? _lastRequest;
    private int _pageIndex = 0;
    readonly List<ISearchParameterSource> _searchParameterSource = new();

    /// <summary>
    /// Specifies a component which may provide search parameters or can start a searching
    /// </summary>
    [Parameter]
    public RenderFragment? Query { get; set; }
    /// <summary>
    /// Specifies a component which may provide search parameters or can start a searching
    /// </summary>
    [Parameter]
    public RenderFragment? OptionsPane { get; set; }
    
    /// <summary>
    /// Specifies a search description
    /// </summary>
    [Parameter]
    public RenderFragment? Description { get; set; }

    /// <summary>
    /// Specifies query page size
    /// </summary>
    [Parameter]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Specifies searching result item template
    /// </summary>
    [Parameter]
    public RenderFragment<T>? ItemTemplate { get; set; }

    /// <summary>
    /// Specifies a searcher
    /// </summary>
    [Parameter]
    public Searcher<T>? Searcher{ get; set; }

    [Inject] 
    private NavigationManager? NavigationManager { get; set; }

    /// <inheritdoc />
    protected override async Task OnInitializedAsync()
    {
        if (NavigationManager != null)
        {
            NavigationManager.LocationChanged += NavigationManagerOnLocationChanged;
        }

        await base.OnInitializedAsync();

        await PerformSearchFromUrlAsync(true);
    }

    private void SearchParameterComponentOnSearchStarted(object? sender, EventArgs e)
    {
        InvokeAsync(() =>
        {
            if (NavigationManager != null)
            {
                var searchRequest = new SearchRequest
                {
                    PageIndex = _pageIndex,
                    PageSize = PageSize
                };

                foreach (var searchParameterComponent in _searchParameterSource)
                    searchParameterComponent.ApplyParameters(searchRequest);

                var targetUrl = searchRequest.AddToUrl(NavigationManager.Uri);
                NavigationManager.NavigateTo(targetUrl);
            }
        });
    }

    private void NavigationManagerOnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        InvokeAsync(async () => await PerformSearchFromUrlAsync(false));
    }

    async Task PerformSearchFromUrlAsync(bool cancelIsEmpty)
    {
        if (NavigationManager != null)
        {
            var searchRequestFromUrl = SearchRequest.ExtractFromQuery(NavigationManager.Uri);

            if (searchRequestFromUrl == null)
            {
                if (cancelIsEmpty) return;

                searchRequestFromUrl = new SearchRequest();
            }
            
            await PerformSearchAsync(searchRequestFromUrl);
        }
    }

    private void PageIndexChanged(int newPageIndex)
    {
        if (_lastRequest != null)
        {
            if (NavigationManager != null)
            {
                var targetUrl = new SearchRequest(_lastRequest)
                {
                    PageIndex = newPageIndex
                }.AddToUrl(NavigationManager);

                NavigationManager.NavigateTo(targetUrl);
            }
        }
    }

    async Task PerformSearchAsync(SearchRequest searchRequest)
    {
        if (Searcher != null)
        {
            try
            {
                _searchResult = await Searcher.SearchAsync(searchRequest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (_searchResult != null)
            {
                _lastRequest = searchRequest;
                _pageIndex = searchRequest.PageIndex;

                StateHasChanged();

                return;
            }
        }

        _lastRequest = null;
        _pageIndex = 0;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (NavigationManager != null)
        {
            NavigationManager.LocationChanged -= NavigationManagerOnLocationChanged;
        }
    }

    /// <inheritdoc />
    public IDisposable RegisterChild(object child)
    {
        if (child is ISearchParameterSource spc)
        {
            _searchParameterSource.Add(spc);
        }

        if (child is ISearchStarter ss)
        {
            ss.SearchStarted += SearchParameterComponentOnSearchStarted;
        }

        return new ChildUnregistrar(child, this);
    }

    class ChildUnregistrar : IDisposable
    {
        private readonly object _child;
        private readonly SearchPage<T> _searchPage;

        public ChildUnregistrar(object child, SearchPage<T> searchPage)
        {
            _child = child;
            _searchPage = searchPage;
        }
        public void Dispose()
        {
            if (_child is ISearchParameterSource spc)
            {
                _searchPage._searchParameterSource.Remove(spc);
            }

            if (_child is ISearchStarter ss)
            {
                ss.SearchStarted -= _searchPage.SearchParameterComponentOnSearchStarted;
            }
        }
    }
}