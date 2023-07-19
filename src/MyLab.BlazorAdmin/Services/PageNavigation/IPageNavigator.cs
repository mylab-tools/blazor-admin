namespace MyLab.BlazorAdmin.Services.PageNavigation;

/// <summary>
/// Provides page navigation
/// </summary>
public interface IPageNavigator
{
    /// <summary>
    /// Gets navigation description
    /// </summary>
    IEnumerable<NavigationCategory> GetNavigation();

    /// <summary>
    /// Gets page description for specified url
    /// </summary>
    public PageNavigation? GetPageDescription(string url);
}