using MyLab.BlazorAdmin.Shared.Dialogs;

namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Extensions for <see cref="IDialogService"/>
/// </summary>
public static class DialogServiceExtensions
{
    /// <summary>
    /// Shows an error message
    /// </summary>
    public static async Task ShowErrorAsync(this IDialogService dialogService, string message)
    {
        await dialogService.Create<ErrorMessage>("Error")
            .BuildOkYesButton().End()
            .WithParameters(() => new () { Messasge = message })
            .OpenAsync();
    }

    /// <summary>
    /// Shows an exclamation message
    /// </summary>
    public static async Task ShowExclamationAsync(this IDialogService dialogService, string message)
    {
        await dialogService.Create<ExclamationMessage>("Exclamation")
            .BuildOkYesButton().End()
            .WithParameters(() => new () { Messasge = message })
            .OpenAsync();
    }

    /// <summary>
    /// Shows Yes-No question
    /// </summary>
    public static Task ShowYnQuestionAsync(
        this IDialogService dialogService,
        string message,
        Action acceptHandler,
        Action? rejectHandler = null)
    {
        return dialogService.Create<QuestionMessage>("Question")
            .BuildOkYesButton().OneWayCallback((_, _, _) => acceptHandler()).End()
            .BuildNoButton().OneWayCallback((_, _, _) => rejectHandler?.Invoke()).End()
            .WithParameters(() => new() { Messasge = message })
            .OpenAsync();
    }

    /// <summary>
    /// Shows Yes-No question
    /// </summary>
    public static Task ShowYnQuestionAsync(
        this IDialogService dialogService, 
        string message,
        Func<Task> acceptHandler,
        Func<Task>? rejectHandler = null)
    {
        return dialogService.Create<QuestionMessage>("Question")
            .BuildOkYesButton().OneWayAsyncCallback((_, _, _) => acceptHandler()).End()
            .BuildNoButton().OneWayAsyncCallback((_, _, _) => rejectHandler?.Invoke() ?? Task.CompletedTask).End()
            .WithParameters(() => new () { Messasge = message })
            .OpenAsync();
    }

    /// <summary>
    /// Shows Yes-No-Cancel question
    /// </summary>
    public static Task ShowYncQuestionAsync(
        this IDialogService dialogService,
        string message,
        Action acceptHandler,
        Action rejectHandler,
        Action? cancelHandler = null)
    {
        return dialogService.Create<QuestionMessage>("Question")
            .BuildOkYesButton().OneWayCallback((_, _, _) => acceptHandler()).End()
            .BuildNoButton().OneWayCallback((_, _, _) => rejectHandler.Invoke()).End()
            .BuildCancelButton().OneWayCallback((_, _, _) => cancelHandler?.Invoke()).End()
            .WithParameters(() => new() { Messasge = message })
            .OpenAsync();
    }

    /// <summary>
    /// Shows Yes-No-Cancel question
    /// </summary>
    public static Task ShowYncQuestionAsync(
        this IDialogService dialogService,
        string message,
        Func<Task> acceptHandler,
        Func<Task> rejectHandler,
        Func<Task>? cancelHandler = null)
    {
        return dialogService.Create<QuestionMessage>("Question")
            .BuildOkYesButton().OneWayAsyncCallback((_, _, _) => acceptHandler()).End()
            .BuildNoButton().OneWayAsyncCallback((_, _, _) => rejectHandler.Invoke()).End()
            .BuildCancelButton().OneWayAsyncCallback((_, _, _) => cancelHandler?.Invoke() ?? Task.CompletedTask).End()
            .WithParameters(() => new() { Messasge = message })
            .OpenAsync();
    }
}