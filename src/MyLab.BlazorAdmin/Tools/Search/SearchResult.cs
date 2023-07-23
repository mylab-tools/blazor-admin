using Newtonsoft.Json;

namespace MyLab.BlazorAdmin.Tools.Search;

/// <summary>
/// Contains search results
/// </summary>
/// <typeparam name="T">item content type</typeparam>
public class SearchResult<T>
{
    /// <summary>
    /// Found items
    /// </summary>
    [JsonProperty("entities")]
    public FoundItem<T>[]? Items { get; set; }

    /// <summary>
    /// Number of total found items without paging
    /// </summary>
    [JsonProperty("total")]
    public long Total { get; set; }
}