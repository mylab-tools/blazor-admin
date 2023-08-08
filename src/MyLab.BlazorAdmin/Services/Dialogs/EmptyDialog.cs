using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Services.Dialogs;

class EmptyDialog<TDialog> : IDialog<TDialog>
{
    public event AsyncEventHandler? Opening;
    public event AsyncEventHandler? Opened;
    public event AsyncEventHandler? Closing;
    public event AsyncEventHandler? Closed;
    public event AsyncEventHandler? ClosePrevented;

    public TDialog Model { get; set; }

    public ValueTask CloseAsync() => ValueTask.CompletedTask;
    public ValueTask OpenAsync() => ValueTask.CompletedTask;
    public DialogResult Result { get; set; }
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}