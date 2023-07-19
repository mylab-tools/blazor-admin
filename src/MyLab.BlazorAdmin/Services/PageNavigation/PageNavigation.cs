namespace MyLab.BlazorAdmin.Services.PageNavigation;

/// <summary>
/// Describes page navigation
/// </summary>
public class PageNavigation
{
    /// <summary>
    /// Page title
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Navigation path to page
    /// </summary>
    public PageNavigationPathItem[] NavPath { get; }

    public PageNavigation(string title, PageNavigationPathItem[] navPath)
    {
        Title = title;
        NavPath = navPath;
    }
}