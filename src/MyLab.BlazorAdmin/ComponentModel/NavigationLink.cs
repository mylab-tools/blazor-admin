namespace MyLab.BlazorAdmin.ComponentModel;

/// <summary>
/// Describes navigation to page
/// </summary>
public class NavigationLink
{
    /// <summary>
    /// Title 
    /// </summary>
    public string? Title { get; init; }
    /// <summary>
    /// target page url
    /// </summary>
    public string? Url { get; init; }
    /// <summary>
    /// Font Awesome class
    /// </summary>
    public string? FaClass { get; init; }
}