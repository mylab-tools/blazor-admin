namespace MyLab.BlazorAdmin.Services.Dialogs;

class DialogButtonBuilder<TDialog> : IDialogButtonBuilder<TDialog>
{
    private readonly DialogButtonDescription _initialDescription;
    private readonly Func<DialogButtonDescription, IDialogBuilder<TDialog>> _addButtonFunc;
    private AsyncDialogCallback? _callback;
    private object? _state;

    public DialogButtonBuilder(
        DialogButtonDescription initialDescription, 
        Func<DialogButtonDescription, IDialogBuilder<TDialog>> addButtonFunc)
    {
        _initialDescription = initialDescription ?? throw new ArgumentNullException(nameof(initialDescription));
        _addButtonFunc = addButtonFunc ?? throw new ArgumentNullException(nameof(addButtonFunc));
    }

    public IDialogButtonBuilder<TDialog> PassState(object state)
    {
        var clone = Clone();
        clone._state = state;
        return clone;
    }

    public IDialogButtonBuilder<TDialog> AsyncCallback(AsyncDialogCallback<TDialog> callback)
    {
        if (callback == null) throw new ArgumentNullException(nameof(callback));
        var clone = Clone();
        clone._callback = (sender, result, state) =>
        {
            TDialog? dialogModel;
            if (sender is TDialog dm)
            {
                dialogModel = dm;
            }
            else
            {
                throw new InvalidCastException($"Dialog type mismatch. Expected '{typeof(TDialog).FullName}' but actual '{sender.GetType().FullName}'");
            }

            return callback(dialogModel, result, state);
        };
        return clone;
    }

    public IDialogBuilder<TDialog> End()
    {
        var newDescription = new DialogButtonDescription
        {
            Callback = _callback ?? _initialDescription.Callback,
            Primary = _initialDescription.Primary,
            Result = _initialDescription.Result,
            State = _state ?? _initialDescription.State,
            Title = _initialDescription.Title
        };

        var newBuilder =  _addButtonFunc(newDescription);

        return newBuilder;
    }

    DialogButtonBuilder<TDialog> Clone()
    {
        return new DialogButtonBuilder<TDialog>(_initialDescription, _addButtonFunc)
        {
            _callback = _callback,
            _state = _state
        };
    }
}