using Newtonsoft.Json;

namespace MyLab.BlazorAdmin.Tools.Search;

/// <summary>
/// Represent found Item
/// </summary>
public class FoundItem<T>
{
    /// <summary>
    /// Item content
    /// </summary>
    [JsonProperty("content")]
    public T? Content { get; set; }
    /// <summary>
    /// Search score
    /// </summary>
    [JsonProperty("score")]
    public float Score { get; set; }
}