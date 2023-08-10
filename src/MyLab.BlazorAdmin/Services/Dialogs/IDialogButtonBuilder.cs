using System.Net.Http.Headers;

namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Builds a dialog button
/// </summary>
public interface IDialogButtonBuilder<TDialog>
{
    /// <summary>
    /// Specifies a state which will be passed into callback
    /// </summary>
    IDialogButtonBuilder<TDialog> PassState(object state);

    /// <summary>
    /// Specifies a callback which will be called when button pressed
    /// </summary>
    IDialogButtonBuilder<TDialog> AsyncCallback(AsyncDialogCallback<TDialog> callback);
    /// <summary>
    /// Ends button building and returns a parent dialog builder
    /// </summary>
    IDialogBuilder<TDialog> End();
}

/// <summary>
/// Extensions for <see cref="IDialogButtonBuilder{TDialog}"/>
/// </summary>
public static class DialogButtonBuilderExtensions
{
    /// <summary>
    /// Specifies a callback which will be called when button pressed
    /// </summary>
    public static IDialogButtonBuilder<TDialog> Callback<TDialog>(this IDialogButtonBuilder<TDialog> builder,
        DialogCallback<TDialog> callback)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));
        if (callback == null) throw new ArgumentNullException(nameof(callback));

        return builder.AsyncCallback((sender, result, state) =>
        {
            var res = callback(sender, result, state);
            return Task.FromResult(res);
        });
    }

    /// <summary>
    /// Specifies a callback which will be called when button pressed
    /// </summary>
    public static IDialogButtonBuilder<TDialog> OneWayAsyncCallback<TDialog>(this IDialogButtonBuilder<TDialog> builder,
        OneWayAsyncDialogCallback<TDialog> callback)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));
        if (callback == null) throw new ArgumentNullException(nameof(callback));

        return builder.AsyncCallback(async (sender, result, state) =>
        {
            await callback(sender, result, state);
            return true;
        });
    }

    /// <summary>
    /// Specifies a callback which will be called when button pressed
    /// </summary>
    public static IDialogButtonBuilder<TDialog> OneWayCallback<TDialog>(this IDialogButtonBuilder<TDialog> builder,
        OneWayDialogCallback<TDialog> callback)
    {
        if (builder == null) throw new ArgumentNullException(nameof(builder));
        if (callback == null) throw new ArgumentNullException(nameof(callback));

        return builder.AsyncCallback((sender, result, state) =>
        {
            callback(sender, result, state);
            return Task.FromResult(true);
        });
    }
}