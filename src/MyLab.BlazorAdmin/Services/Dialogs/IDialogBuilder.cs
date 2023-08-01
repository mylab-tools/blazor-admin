using System.Linq.Expressions;
using MyLab.BlazorAdmin.Shared.Dialogs;

namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Builds dialog <see cref="DialogDescription"/>
/// </summary>
public interface IDialogBuilder<TContent>
{
    /// <summary>
    /// Adds OkYes button
    /// </summary>
    public IDialogBuilder<TContent> WithOkYesButton(Action callback, string title = "OK");
    /// <summary>
    /// Adds No button
    /// </summary>
    public IDialogBuilder<TContent> WithNoButton(Action callback, string title = "No");
    /// <summary>
    /// Adds cancel button
    /// </summary>
    public IDialogBuilder<TContent> WithCancelButton(string title = "Cancel");
    /// <summary>
    /// Adds custom button
    /// </summary>
    public IDialogBuilder<TContent> WithButton(DialogButtonDescription description);
    /// <summary>
    /// Specifies a footer component
    /// </summary>
    /// <typeparam name="TFooter">component type</typeparam>
    /// <param name="setParams">member init expression</param>
    public IDialogBuilder<TContent> WithFooter<TFooter>(Expression<Func<TFooter>>? setParams = null);
    /// <summary>
    /// Specifies the footer initial parameters
    /// </summary>
    /// <param name="setParams">member init expression</param>
    public IDialogBuilder<TContent> WithParameters(Expression<Func<TContent>> setParams);
    /// <summary>
    /// Specifies a modal-backdrop element 
    /// </summary>
    public IDialogBuilder<TContent> WithBackdrop(DialogBackdrop backdrop);
    /// <summary>
    /// Opens a dialog
    /// </summary>
    public IDialog Create();
}