using System.Collections.Immutable;
using System.Linq.Expressions;
using MyLab.BlazorAdmin.Shared.Dialogs;
using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Services.Dialogs;

class DialogBuilder<TContent> : IDialogBuilder<TContent>
{
    readonly string _title;

    private readonly IDialogPlace? _dialogPlace;

    private List<DialogButtonDescription> _customButtons = new();

    private DialogButtonDescription? _okYesBtn;
    private DialogButtonDescription? _noBtn;
    private DialogButtonDescription? _cancelBtn;

    private Type _contentType;

    private Type? _footerType;

    private DialogBackdrop? _backdrop;

    private InitParametersDictionary? _contentParameters;

    private InitParametersDictionary? _footerParameters;

    private DialogCallback? _dialogCallback;
        
    public DialogBuilder(string title, IDialogPlace? dialogPlace)
    {
        _title = title ?? throw new ArgumentNullException(nameof(title));
        _dialogPlace = dialogPlace;
        _contentType = typeof(TContent);
    }

    public IDialogBuilder<TContent> WithDialogCallback(DialogCallback callback)
    {
        var clone = Clone();

        clone._dialogCallback = callback;

        return clone;
    }

    public IDialogBuilder<TContent> WithOkYesButton(DialogCallback callback, object? state = null)
    {
        var clone = Clone();
            
        clone._okYesBtn = new DialogButtonDescription
        {
            Callback = callback, 
            Primary = true,
            State = state,
            Result = DialogResult.OkYes
        };

        return clone;
    }

    public IDialogBuilder<TContent> WithNoButton(DialogCallback? callback = null, object? state = null)
    {
        var clone = Clone();

        clone._noBtn = new DialogButtonDescription
        {
            Callback = callback,
            State = state,
            Result = DialogResult.No
        };

        return clone;
    }

    public IDialogBuilder<TContent> WithCancelButton(DialogCallback? callback = null, object? state = null)
    {
        var clone = Clone();

        clone._cancelBtn = new DialogButtonDescription
        {
            Callback = callback,
            State = state,
            Result = DialogResult.Undefined
        };

        return clone;
    }

    public IDialogBuilder<TContent> AddButton(DialogButtonDescription description)
    {
        if (description == null) throw new ArgumentNullException(nameof(description));

        var clone = Clone();

        clone._customButtons.Add(description);

        return clone;
    }

    public IDialogBuilder<TContent> WithFooter<TFooter>(Expression<Func<TFooter>>? setParams = null)
    {
        var clone = Clone();

        clone._footerType = typeof(TFooter);

        if (setParams != null)
        {
            clone._footerParameters = InitParametersDictionary.FromExpression(setParams);
        }

        return clone;
    }

    public IDialogBuilder<TContent> WithParameters(Expression<Func<TContent>> setParams)
    {
        if (setParams == null) throw new ArgumentNullException(nameof(setParams));

        var clone = Clone();

        clone._contentParameters = InitParametersDictionary.FromExpression(setParams);

        return clone;
    }

    public IDialogBuilder<TContent> WithBackdrop(DialogBackdrop backdrop)
    {
        var clone = Clone();

        clone._backdrop = backdrop;

        return clone;
    }

    public async Task<IDialog> CreateAsync()
    {
        if (_dialogPlace == null) return new EmptyDialog();
        return await _dialogPlace.CreateDialogAsync(ToDescription());
    }

    DialogDescription ToDescription()
    {
        return new DialogDescription(_contentType)
        {
            Title = _title,
            Backdrop = _backdrop ?? DialogBackdrop.False,
            FooterType = _footerType,
            CustomButtons = _customButtons.ToImmutableArray(),
            ContentParameters = _contentParameters,
            FooterParameters = _footerParameters,
            DialogCallback = _dialogCallback,
            CancelButton = _cancelBtn,
            NoButton = _noBtn,
            OkYesButton = _okYesBtn
        };
    }

    DialogBuilder<TContent> Clone()
    {
        return new DialogBuilder<TContent>(_title, _dialogPlace)
        {
            _backdrop = _backdrop,
            _contentParameters = _contentParameters,
            _footerParameters = _footerParameters,
            _customButtons = _customButtons,
            _contentType = _contentType,
            _footerType = _footerType,
            _dialogCallback = _dialogCallback,
            _cancelBtn = _cancelBtn,
            _noBtn = _noBtn,
            _okYesBtn = _okYesBtn
        };
    }
}