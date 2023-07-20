using MyLab.BlazorAdmin.ComponentModel;

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
    public NavigationLink[]? Pages { get; set; }
}