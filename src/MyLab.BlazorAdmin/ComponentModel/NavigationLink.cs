namespace MyLab.BlazorAdmin.ComponentModel;

/// <summary>
/// Describes navigation to page
/// </summary>
/// <param name="Title">Title</param>
/// <param name="Url">Target page url</param>
/// <param name="FaClass">Font Awesome class</param>
public record NavigationLink(string Title, string? Url, string? FaClass)
    : Link(Title, Url ?? "#");