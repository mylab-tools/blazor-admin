using System.Collections.Immutable;
using System.Linq.Expressions;
using MyLab.BlazorAdmin.Shared.Dialogs;
using MyLab.BlazorAdmin.Tools;
using MyLab.BlazorAdmin.Tools.Rendering;

namespace MyLab.BlazorAdmin.Services.Dialogs;

class DialogBuilder<TDialog> : IDialogBuilder<TDialog>
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

    private AsyncDialogCallback? _dialogCallback;
        
    public DialogBuilder(string title, IDialogPlace? dialogPlace)
    {
        _title = title ?? throw new ArgumentNullException(nameof(title));
        _dialogPlace = dialogPlace;
        _contentType = typeof(TDialog);
    }

    public IDialogBuilder<TDialog> WithDialogCallback(AsyncDialogCallback<TDialog> callback)
    {
        if (callback == null) throw new ArgumentNullException(nameof(callback));
        var clone = Clone();

        clone._dialogCallback = (sender, result, state) => callback((TDialog)sender, result, state);

        return clone;
    }

    public IDialogButtonBuilder<TDialog> BuildOkYesButton()
    {
        return new DialogButtonBuilder<TDialog>(
            new DialogButtonDescription
            {
                Primary = true,
                Result = DialogResult.OkYes
            },
            newDesc =>
            {
                var clone = Clone();

                clone._okYesBtn = newDesc;

                return clone;
            });
    }

    public IDialogButtonBuilder<TDialog> BuildNoButton()
    {
        return new DialogButtonBuilder<TDialog>(
            new DialogButtonDescription
            {
                Result = DialogResult.No
            },
            newDesc =>
            {
                var clone = Clone();

                clone._noBtn = newDesc;

                return clone;
            });
    }

    public IDialogButtonBuilder<TDialog> BuildCancelButton()
    {
        return new DialogButtonBuilder<TDialog>(
            new DialogButtonDescription(),
            newDesc =>
            {
                var clone = Clone();

                clone._cancelBtn = newDesc;

                return clone;
            });
    }
    
    public IDialogButtonBuilder<TDialog> BuildButton(string? title = null, bool primary = false, DialogResult dialogResult = DialogResult.Undefined)
    {
        return new DialogButtonBuilder<TDialog>(
            new DialogButtonDescription
            {
                Title = title,
                Primary = primary,
                Result = dialogResult
            },
            newDesc =>
            {
                var clone = Clone();

                clone._customButtons.Add(newDesc);

                return clone;
            });
    }

    public IDialogBuilder<TDialog> AddButton(DialogButtonDescription description)
    {
        if (description == null) throw new ArgumentNullException(nameof(description));

        var clone = Clone();

        clone._customButtons.Add(description);

        return clone;
    }

    public IDialogBuilder<TDialog> WithFooter<TFooter>(Expression<Func<TFooter>>? setParams = null)
    {
        var clone = Clone();

        clone._footerType = typeof(TFooter);

        if (setParams != null)
        {
            clone._footerParameters = InitParametersDictionary.FromExpression(setParams);
        }

        return clone;
    }

    public IDialogBuilder<TDialog> WithParameters(Expression<Func<TDialog>> setParams)
    {
        if (setParams == null) throw new ArgumentNullException(nameof(setParams));

        var clone = Clone();

        clone._contentParameters = InitParametersDictionary.FromExpression(setParams);

        return clone;
    }

    public IDialogBuilder<TDialog> WithBackdrop(DialogBackdrop backdrop)
    {
        var clone = Clone();

        clone._backdrop = backdrop;

        return clone;
    }

    public async Task<IDialog<TDialog>> CreateAsync()
    {
        if (_dialogPlace == null) return new EmptyDialog<TDialog>();
        return await _dialogPlace.CreateDialogAsync<TDialog>(ToDescription());
    }

    DialogDescription ToDescription()
    {
        return new DialogDescription(new TemplateRenderFragmentFactory(_contentType, _contentParameters))
        {
            Title = _title,
            Backdrop = _backdrop ?? DialogBackdrop.False,
            CustomButtons = _customButtons.ToImmutableArray(),
            Footer = _footerType != null ? new TemplateRenderFragmentFactory(_footerType, _footerParameters) : null,
            DialogCallback = _dialogCallback,
            CancelButton = _cancelBtn,
            NoButton = _noBtn,
            OkYesButton = _okYesBtn
        };
    }

    DialogBuilder<TDialog> Clone()
    {
        return new DialogBuilder<TDialog>(_title, _dialogPlace)
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