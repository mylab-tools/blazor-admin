namespace MyLab.BlazorAdmin.Services.PageNavigation;

/// <summary>
/// Defines navigation node
/// </summary>
public class PageNavigationNodeDefinition
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; init; }
    /// <summary>
    /// Url path item
    /// </summary>
    public string? UrlItem { get; init; }
    /// <summary>
    /// Font Awesome class
    /// </summary>
    public string? FaClass { get; init; }
    /// <summary>
    /// Nested nodes
    /// </summary>
    public PageNavigationNodeDefinition[]? Nodes { get; set; }
}