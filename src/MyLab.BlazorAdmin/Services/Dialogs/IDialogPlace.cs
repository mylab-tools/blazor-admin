namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Represent a dialog place
/// </summary>
public interface IDialogPlace
{
    /// <summary>
    /// Creates and initialize new dialog
    /// </summary>
    Task<IDialog> CreateDialogAsync(DialogDescription description);
}