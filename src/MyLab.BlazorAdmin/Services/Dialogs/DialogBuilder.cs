using System.Collections.Immutable;
using System.Linq.Expressions;
using MyLab.BlazorAdmin.Shared.Dialogs;
using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Services.Dialogs;

class DialogBuilder<TContent> : IDialogBuilder<TContent>
{
    readonly string _title;

    private readonly IDialogPlace _dialogPlace;

    private List<DialogButtonDescription> _buttons = new();

    private Type _contentType;

    private Type? _footerType;

    private DialogBackdrop? _backdrop;

    private InitParametersDictionary? _contentParameters;

    private InitParametersDictionary? _footerParameters;
        
    public DialogBuilder(string title, IDialogPlace dialogPlace)
    {
        _title = title ?? throw new ArgumentNullException(nameof(title));
        _dialogPlace = dialogPlace;
        _contentType = typeof(TContent);
    }

    public IDialogBuilder<TContent> WithOkYesButton(Action callback, string title = "OK")
    {
        var clone = Clone();
            
        clone._buttons.Add(new DialogButtonDescription(title){ Callback = callback, Primary = true});

        return clone;
    }

    public IDialogBuilder<TContent> WithNoButton(Action callback, string title = "No")
    {
        var clone = Clone();

        clone._buttons.Add(new DialogButtonDescription(title) { Callback = callback });

        return clone;
    }

    public IDialogBuilder<TContent> WithCancelButton(string title = "Cancel")
    {
        var clone = Clone();

        clone._buttons.Add(new DialogButtonDescription(title));

        return clone;
    }

    public IDialogBuilder<TContent> WithButton(DialogButtonDescription description)
    {
        if (description == null) throw new ArgumentNullException(nameof(description));

        var clone = Clone();

        clone._buttons.Add(description);

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

    public IDialog Create()
    {
        return _dialogPlace.CreateDialog(ToDescription());
    }

    DialogDescription ToDescription()
    {
        return new DialogDescription(_contentType)
        {
            Title = _title,
            Backdrop = _backdrop ?? DialogBackdrop.False,
            FooterType = _footerType,
            Buttons = _buttons.ToImmutableArray(),
            ContentParameters = _contentParameters,
            FooterParameters = _footerParameters
        };
    }

    DialogBuilder<TContent> Clone()
    {
        return new DialogBuilder<TContent>(_title, _dialogPlace)
        {
            _backdrop = _backdrop,
            _contentParameters = _contentParameters,
            _footerParameters = _footerParameters,
            _buttons = _buttons,
            _contentType = _contentType,
            _footerType = _footerType
        };
    }
}