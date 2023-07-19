namespace MyLab.BlazorAdmin.Services.PageNavigation;

/// <summary>
/// Describes navigation to page
/// </summary>
public class PageNavigationLink
{
    public string? Title { get; init; }
    public string? Url { get; init; }
    public string? FaClass { get; init; }
}