using Microsoft.AspNetCore.Components;
using MyLab.BlazorAdmin.Tools.Search;

namespace MyLab.BlazorAdmin.Shared.Search
{
    /// <summary>
    /// Visualize search result
    /// </summary>
    /// <typeparam name="T">item type</typeparam>
    public partial class SearchResultTable<T>
    {
        private SearchResult<T>? _searchResult;
        private int _pageIndex = 0;

        bool _prevPageEnabled;
        bool _nextPageEnabled;

        /// <summary>
        /// Gets or sets current search result
        /// </summary>
        [Parameter]
        public SearchResult<T>? SearchResult
        {
            get => _searchResult;
            set
            {
                _searchResult = value;
                UpdateFlipButtonStates();
            }
        }

        /// <summary>
        /// Gets or sets current page index
        /// </summary>
        [Parameter]
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                _pageIndex = value;
                UpdateFlipButtonStates();
            }
        }

        /// <summary>
        /// Gets or sets maximum query size
        /// </summary>
        [Parameter]
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Found item template
        /// </summary>
        [Parameter]
        public RenderFragment<T>? ItemTemplate { get; set; }

        /// <summary>
        /// Occurs when page index changed
        /// </summary>
        [Parameter]
        public EventCallback<int> PageIndexChanged { get; set; }

        long GetTotalPageNumber()
        {
            if (SearchResult == null) return 0;
            return PageSize != 0 ? SearchResult.Total / PageSize : 0;
        }

        void UpdateFlipButtonStates()
        {
            _prevPageEnabled = _searchResult != null && _pageIndex > 0;
            _nextPageEnabled = _searchResult != null && _pageIndex * PageSize < _searchResult.Total;
        }

        private Task OnPrevPageHandledAsync()
        {
            return PageIndexChanged.InvokeAsync(PageIndex - 1);
        }

        private Task OnNextPageHandledAsync()
        {
            return PageIndexChanged.InvokeAsync(PageIndex + 1);
        }
    }
}
