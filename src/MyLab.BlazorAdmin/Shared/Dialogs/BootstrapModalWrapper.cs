using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MyLab.BlazorAdmin.Shared.Dialogs;

class BootstrapModalWrapper
{
    private readonly IJSRuntime _js;
    private readonly string _elementId;
    private readonly DotNetObjectReference<BootstrapModalWrapper> _ref;

    public EventCallback OnShow;
    public EventCallback OnShown;
    public EventCallback OnHide;
    public EventCallback OnHidden;
    public EventCallback OnHidePrevented;

    [JSInvokable]
    public async Task OnShowModal() => await OnShow.InvokeAsync();
    [JSInvokable]
    public async Task OnShownModal() => await OnShown.InvokeAsync();
    [JSInvokable]
    public async Task OnHideModal() => await OnHide.InvokeAsync();
    [JSInvokable]
    public async Task OnHiddenModal() => await OnHidden.InvokeAsync();
    [JSInvokable]
    public async Task OnHidePreventedModal() => await OnHidePrevented.InvokeAsync();

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

    public ValueTask ShowAsync()
    {
        return _js.InvokeVoidAsync("window.mylabAdmin.modal.show", _elementId);
    }

    public ValueTask HideAsync()
    {
        return _js.InvokeVoidAsync("window.mylabAdmin.modal.hide", _elementId);
    }

    public ValueTask DisposeAsync()
    {
        return _js.InvokeVoidAsync("window.mylabAdmin.modal.dispose", _elementId);
    }
}