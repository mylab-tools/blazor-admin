namespace MyLab.BlazorAdmin.Shared.Search;

/// <summary>
/// Describes a searcher sorting
/// </summary>
public class SortingDesc
{
    /// <summary>
    /// Sorting identifier or null of default
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="SortingDesc"/>
    /// </summary>
    public SortingDesc(string? id, string title)
    {
        Id = id;
        Title = title;
    }

    /// <summary>
    /// Creates default sorting
    /// </summary>
    public FilterDesc CreateDefault(string title)
    {
        return new FilterDesc(null, title);
    }
}