namespace MyLab.BlazorAdmin.Shared.Search;

/// <summary>
/// Defines searching initiator
/// </summary>
public interface ISearchStarter
{
    /// <summary>
    /// Occurs when an object initiate a new searching
    /// </summary>
    event EventHandler SearchStarted;
}