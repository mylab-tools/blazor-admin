namespace MyLab.BlazorAdmin.Services.PageNavigation;

/// <summary>
/// Contains navigation path item properties
/// </summary>
public class PageNavigationPathItem
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Url
    /// </summary>
    public string Url { get; }

    public PageNavigationPathItem(string title, string url)
    {
        Title = title;
        Url = url;
    }
}