using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MyLab.BlazorAdmin.Services.Dialogs;
using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Shared.Dialogs;

class BootstrapModalWrapper : IDialog
{
    private readonly IJSRuntime _js;
    private readonly string _elementId;
    private readonly DotNetObjectReference<BootstrapModalWrapper> _ref;

    public event AsyncEventHandler? Opening;
    public event AsyncEventHandler? Opened;
    public event AsyncEventHandler? Closing;
    public event AsyncEventHandler? Closed;
    public event AsyncEventHandler? ClosePrevented;

    [JSInvokable]
    public async Task OnShowModal()
    {
        if (Opening != null)
            await Opening(this, EventArgs.Empty);
    }

    [JSInvokable]
    public async Task OnShownModal()
    {
        if (Opened != null)
            await Opened(this, EventArgs.Empty);
    }

    [JSInvokable]
    public async Task OnHideModal()
    {
        if (Closing != null)
            await Closing(this, EventArgs.Empty);
    }

    [JSInvokable]
    public async Task OnHiddenModal()
    {
        if (Closed != null)
            await Closed(this, EventArgs.Empty);
    }

    [JSInvokable]
    public async Task OnHidePreventedModal()
    {
        if (ClosePrevented != null)
            await ClosePrevented(this, EventArgs.Empty);
    }

    /// <summary>
    /// Initializes a new instance of <see cref="BootstrapModalWrapper"/>
    /// </summary>
    public BootstrapModalWrapper(IJSRuntime js, string elementId)
    {
        _js = js ?? throw new ArgumentNullException(nameof(js));
        _elementId = elementId ?? throw new ArgumentNullException(nameof(elementId));

        _ref = DotNetObjectReference.Create(this); 
    }

    public ValueTask InitializeAsync(DialogBackdrop backdrop)
    {
        object backdropObj;

        switch (backdrop)
        {
            case DialogBackdrop.False:
                backdropObj = false;
                break;
            case DialogBackdrop.True:
                backdropObj = true;
                break;
            case DialogBackdrop.Static:
                backdropObj = "static";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(backdrop), backdrop, null);
        }

        return _js.InvokeVoidAsync("window.mylabAdmin.modal.initialize", _elementId, backdropObj, _ref);
    }

    public ValueTask OpenAsync()
    {
        return _js.InvokeVoidAsync("window.mylabAdmin.modal.show", _elementId);
    }

    public ValueTask CloseAsync()
    {
        return _js.InvokeVoidAsync("window.mylabAdmin.modal.hide", _elementId);
    }

    public ValueTask DisposeAsync()
    {
        return _js.InvokeVoidAsync("window.mylabAdmin.modal.dispose", _elementId);
    }
}