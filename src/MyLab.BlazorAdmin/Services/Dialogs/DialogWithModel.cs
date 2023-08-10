using MyLab.BlazorAdmin.Shared.Dialogs;
using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Services.Dialogs;

class DialogWithModel<TModel> : IDialog<TModel>
{
    private readonly BootstrapModalWrapper _nested;
    
    public event AsyncEventHandler? Opening
    {
        add => _nested.Opening += value;
        remove => _nested.Opening -= value;
    }

    public event AsyncEventHandler? Opened
    {
        add => _nested.Opened += value;
        remove => _nested.Opened -= value;
    }

    public event AsyncEventHandler? Closing
    {
        add => _nested.Closing += value;
        remove => _nested.Closing -= value;
    }

    public event AsyncEventHandler? Closed
    {
        add => _nested.Closed += value; 
        remove => _nested.Closed -= value;
    }

    public event AsyncEventHandler? ClosePrevented
    {
        add => _nested.ClosePrevented += value; 
        remove => _nested.ClosePrevented -= value;
    }

    public DialogResult Result
    {
        get => _nested.Result;
        set => _nested.Result = value;
    }

    public TModel? Model { get; set; }

    public DialogWithModel(BootstrapModalWrapper nested)
    {
        _nested = nested;
    }

    public ValueTask DisposeAsync()
    {
        return _nested.DisposeAsync();
    }


    public ValueTask CloseAsync()
    {
        return _nested.CloseAsync();
    }

    public ValueTask OpenAsync()
    {
        return _nested.OpenAsync();
    }
}