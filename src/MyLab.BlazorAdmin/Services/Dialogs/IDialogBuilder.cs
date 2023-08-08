using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MyLab.BlazorAdmin.Shared.Dialogs;

namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Builds dialog <see cref="DialogDescription"/>
/// </summary>
public interface IDialogBuilder<TDialog>
{
    /// <summary>
    /// Sets callback for all results
    /// </summary>
    IDialogBuilder<TDialog> WithDialogCallback(AsyncDialogCallback<TDialog> callback);
    /// <summary>
    /// Sets OkYes button
    /// </summary>
    public IDialogButtonBuilder<TDialog> BuildOkYesButton();
    /// <summary>
    /// Sets No button
    /// </summary>
    public IDialogButtonBuilder<TDialog> BuildNoButton();
    /// <summary>
    /// Sets cancel button
    /// </summary>
    public IDialogButtonBuilder<TDialog> BuildCancelButton();
    /// <summary>
    /// Adds new button and creates a builder
    /// </summary>
    public IDialogButtonBuilder<TDialog> BuildButton(string? title = null, bool primary = false, DialogResult dialogResult = DialogResult.Undefined);
    /// <summary>
    /// Adds custom button
    /// </summary>
    public IDialogBuilder<TDialog> AddButton(DialogButtonDescription description);
    /// <summary>
    /// Specifies a footer component
    /// </summary>
    /// <typeparam name="TFooter">component type</typeparam>
    /// <param name="setParams">member init expression</param>
    public IDialogBuilder<TDialog> WithFooter<TFooter>(Expression<Func<TFooter>>? setParams = null);
    /// <summary>
    /// Specifies the footer initial parameters
    /// </summary>
    /// <param name="setParams">member init expression</param>
    public IDialogBuilder<TDialog> WithParameters(Expression<Func<TDialog>> setParams);
    /// <summary>
    /// Specifies a modal-backdrop element 
    /// </summary>
    public IDialogBuilder<TDialog> WithBackdrop(DialogBackdrop backdrop);
    /// <summary>
    /// Creates a dialog
    /// </summary>
    public Task<IDialog<TDialog>> CreateAsync();
}

/// <summary>
/// Extensions for <see cref="IDialogBuilder{TDialog}"/>
/// </summary>
public static class DialogBuilderExtensions
{
    /// <summary>
    /// Creates and opens a dialog
    /// </summary>
    public static async Task<IDialog<TDialog>> OpenAsync<TDialog>(this IDialogBuilder<TDialog> builder)
    {
        var dialog = await builder.CreateAsync();
        await dialog.OpenAsync();
        return dialog;
    }

    /// <summary>
    /// Sets OkYes button
    /// </summary>
    public static IDialogBuilder<TDialog> WithOkYesButton<TDialog>(this IDialogBuilder<TDialog> builder)
    {
        return builder.BuildOkYesButton().End();
    }

    /// <summary>
    /// Sets No button
    /// </summary>
    public static IDialogBuilder<TDialog> WithNoButton<TDialog>(this IDialogBuilder<TDialog> builder)
    {
        return builder.BuildNoButton().End();
    }

    /// <summary>
    /// Sets cancel button
    /// </summary>
    public static IDialogBuilder<TDialog> WithCancelButton<TDialog>(this IDialogBuilder<TDialog> builder)
    {
        return builder.BuildCancelButton().End();
    }
}