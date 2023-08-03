namespace MyLab.BlazorAdmin.Services.Dialogs;

class DialogService : IDialogService, IDialogPlaceRegistrar
{
    private IDialogPlace _dialogPlace;

    public void Register(IDialogPlace dialogPlace)
    {
        _dialogPlace = dialogPlace ?? throw new ArgumentNullException(nameof(dialogPlace));
    }

    public IDialogBuilder<TContent> Create<TContent>(string title)
    {
        return new DialogBuilder<TContent>(title, _dialogPlace);
    }
}