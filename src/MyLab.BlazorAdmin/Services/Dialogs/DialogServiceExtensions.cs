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
            .WithOkYesButton((_, _) => Task.FromResult(true))
            .WithParameters(() => new () { Messasge = message })
            .OpenAsync();
    }

    /// <summary>
    /// Shows an exclamation message
    /// </summary>
    public static async Task ShowExclamationAsync(this IDialogService dialogService, string message)
    {
        await dialogService.Create<ExclamationMessage>("Exclamation")
            .WithOkYesButton((_, _) => Task.FromResult(true))
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
        var aFunc = () =>
        {
            acceptHandler();
            return Task.CompletedTask;
        };

        Func<Task>? rFunc = null;

        if (rejectHandler != null)
        {
            rFunc = () =>
            {
                rejectHandler();
                return Task.CompletedTask;
            };
        }

        return ShowYnQuestionAsync(dialogService, message, aFunc, rFunc);
    }

    /// <summary>
    /// Shows Yes-No question
    /// </summary>
    public static async Task ShowYnQuestionAsync(
        this IDialogService dialogService, 
        string message,
        Func<Task> acceptHandler,
        Func<Task>? rejectHandler = null)
    {
        await dialogService.Create<QuestionMessage>("Error")
            .WithOkYesButton(async (_, _) =>
            {
                try
                {
                    await acceptHandler();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return true;
            })
            .WithNoButton(async (_, _) =>
            {
                if (rejectHandler != null)
                {
                    try
                    {
                        await rejectHandler();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return true;
            })
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
        var aFunc = () =>
        {
            acceptHandler();
            return Task.CompletedTask;
        };

        var rFunc = () =>
        {
            rejectHandler();
            return Task.CompletedTask;
        };

        Func<Task>? cFunc = null;

        if (cancelHandler != null)
        {
            cFunc = () =>
            {
                cancelHandler();
                return Task.CompletedTask;
            };
        }

        return ShowYncQuestionAsync(dialogService, message, aFunc, rFunc, cFunc);
    }

    /// <summary>
    /// Shows Yes-No-Cancel question
    /// </summary>
    public static async Task ShowYncQuestionAsync(
        this IDialogService dialogService,
        string message,
        Func<Task> acceptHandler,
        Func<Task> rejectHandler,
        Func<Task>? cancelHandler = null)
    {
        await dialogService.Create<QuestionMessage>("Error")
            .WithOkYesButton( async (_, _) =>
            {
                try
                {
                    await acceptHandler();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return true;
            })
            .WithNoButton(async (_, _) =>
            {
                try
                {
                    await rejectHandler();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return true;
            })
            .WithCancelButton(async (_, _) =>
            {
                if (cancelHandler != null)
                {
                    try
                    {
                        await cancelHandler();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return true;
            })
            .WithParameters(() => new(){ Messasge = message })
            .OpenAsync();
    }
}