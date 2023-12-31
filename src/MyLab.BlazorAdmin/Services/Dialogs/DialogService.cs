﻿namespace MyLab.BlazorAdmin.Services.Dialogs;

class DialogService : IDialogService, IDialogPlaceRegistrar
{
    private IDialogPlace _dialogPlace;

    public void Register(IDialogPlace dialogPlace)
    {
        _dialogPlace = dialogPlace ?? throw new ArgumentNullException(nameof(dialogPlace));
    }

    public IDialogBuilder<TDialog> Create<TDialog>(string title)
    {
        return new DialogBuilder<TDialog>(title, _dialogPlace);
    }
}