namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Registers a dialog place
/// </summary>
public interface IDialogPlaceRegistrar
{
    /// <summary>
    /// Registers dialog place
    /// </summary>
    void Register(IDialogPlace dialogPlace);
}