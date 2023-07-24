using System.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging.Abstractions;

namespace MyLab.BlazorAdmin.Tools.Search;

/// <summary>
/// Contains search parameters
/// </summary>
public class SearchRequest
{
    private static readonly string[] AllKeys = { "ps", "pi", "f", "q", "s" };

    /// <summary>
    /// String query
    /// </summary>
    public string? Query { get; set; }
    /// <summary>
    /// Sorting identifier
    /// </summary>
    public string? SortId { get; set; }
    /// <summary>
    /// Filter identifier
    /// </summary>
    public string? FilterId { get; set; }

    /// <summary>
    /// Page number
    /// </summary>
    public int PageIndex { get; set; }
    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="SearchRequest"/>
    /// </summary>
    public SearchRequest()
    {

    }

    /// <summary>
    /// Initializes a new instance of <see cref="SearchRequest"/>
    /// </summary>
    public SearchRequest(SearchRequest initial)
    {
        Query = initial.Query;
        SortId = initial.SortId;
        FilterId = initial.FilterId;
        PageIndex = initial.PageIndex;
        PageSize = initial.PageSize;
    }

    /// <summary>
    /// Adds query parameters to specified url
    /// </summary>
    public string AddToUrl(string url)
    {
        var originUrl = new Uri(url);
        var originQueryCollection = HttpUtility.ParseQueryString(originUrl.Query);
        
        if (PageIndex >= 0)
            originQueryCollection["pi"] = PageIndex.ToString();
        if (PageSize >= 0)
            originQueryCollection["ps"] = PageSize.ToString();
        if (FilterId != null)
            originQueryCollection["f"] = FilterId;
        if (SortId != null)
            originQueryCollection["s"] = SortId;
        if (Query != null)
            originQueryCollection["q"] = Query;

        if (originQueryCollection.Count == 0) return url;

        var queryItems = originQueryCollection.AllKeys.Select(k => $"{k}={originQueryCollection[k]}");
        UriBuilder ub = new UriBuilder(url)
        {
            Query = string.Join('&', queryItems)
        };
        
        return ub.Uri.ToString();
    }

    /// <summary>
    /// Adds query parameters to specified url
    /// </summary>
    public string AddToUrl(NavigationManager navigationManager)
    {
        return navigationManager.GetUriWithQueryParameters(
            new Dictionary<string, object?>
            {
                ["pi"] = PageIndex,
                ["ps"] = PageSize,
                ["f"] = FilterId,
                ["s"] = SortId,
                ["q"] = Query
            });
    }


    /// <summary>
    /// Extracts request from query string
    /// </summary>
    public static SearchRequest? ExtractFromQuery(string uri)
    {
        var query = new Uri(uri).Query;
        var queryItems = HttpUtility.ParseQueryString(query);

        if (queryItems.AllKeys.All(k => !AllKeys.Contains(k)))
            return null;

        string? pageSizeStr = queryItems["ps"];
        string? pageIndexStr = queryItems["pi"];

        return new SearchRequest
        {
            PageSize = pageSizeStr != null ? int.Parse(pageSizeStr) : 0,
            PageIndex = pageIndexStr != null ? int.Parse(pageIndexStr) : 0,
            FilterId = queryItems["f"],
            Query = queryItems["q"],
            SortId = queryItems["s"]
        };
    }
}