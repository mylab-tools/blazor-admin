namespace MyLab.BlazorAdmin.Services.PageNavigation;

/// <summary>
/// Describes navigation category
/// </summary>
public class NavigationCategory
{
    /// <summary>
    /// Title
    /// </summary>
    public string? Title { get; init; }
        
    /// <summary>
    /// Nested pages
    /// </summary>
    public PageNavigationLink[]? Pages { get; set; }
}