using System.Linq.Expressions;
using MyLab.BlazorAdmin.Shared.Dialogs;

namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Builds dialog <see cref="DialogDescription"/>
/// </summary>
public interface IDialogBuilder<TContent>
{
    /// <summary>
    /// Sets callback for all results
    /// </summary>
    IDialogBuilder<TContent> WithDialogCallback(DialogCallback callback);
    /// <summary>
    /// Sets OkYes button
    /// </summary>
    public IDialogBuilder<TContent> WithOkYesButton(DialogCallback callback, object? state = null);
    /// <summary>
    /// Sets No button
    /// </summary>
    public IDialogBuilder<TContent> WithNoButton(DialogCallback? callback = null, object? state = null);
    /// <summary>
    /// Sets cancel button
    /// </summary>
    public IDialogBuilder<TContent> WithCancelButton(DialogCallback? callback = null, object? state = null);
    /// <summary>
    /// Adds custom button
    /// </summary>
    public IDialogBuilder<TContent> AddButton(DialogButtonDescription description);
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
    /// Creates a dialog
    /// </summary>
    public Task<IDialog> CreateAsync();
}

/// <summary>
/// Extensions for <see cref="IDialogBuilder{TContent}"/>
/// </summary>
public static class DialogBuilderExtensions
{
    /// <summary>
    /// Creates and opens a dialog
    /// </summary>
    public static async Task<IDialog> OpenAsync<TContent>(this IDialogBuilder<TContent> builder)
    {
        var dialog = await builder.CreateAsync();
        await dialog.OpenAsync();
        return dialog;
    }
}