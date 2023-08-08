using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Represent a dialog
/// </summary>
public interface IDialog : IAsyncDisposable
{
    /// <summary>
    /// Occurs when the dialog begin to open
    /// </summary>
    event AsyncEventHandler Opening;
    /// <summary>
    /// Occurs when the dialog just opened
    /// </summary>
    event AsyncEventHandler Opened;
    /// <summary>
    /// Occurs when the dialog begin to close
    /// </summary>
    event AsyncEventHandler Closing;
    /// <summary>
    /// Occurs when the dialog just closed
    /// </summary>
    event AsyncEventHandler Closed;
    /// <summary>
    /// Occurs when the dialog is shown, its backdrop is static and a click outside the dialog
    /// </summary>
    event AsyncEventHandler ClosePrevented;
    /// <summary>
    /// Closes the dialog
    /// </summary>
    ValueTask CloseAsync();
    /// <summary>
    /// Opens the dialog
    /// </summary>
    /// <returns></returns>
    ValueTask OpenAsync();
    /// <summary>
    /// Gets or sets a dialog result
    /// </summary>
    DialogResult Result { get; set; }
}

/// <summary>
/// Represent a dialog
/// </summary>
public interface IDialog<out TModel> : IDialog
{
    /// <summary>
    /// Gets a dialog model
    /// </summary>
    TModel Model { get; }
}