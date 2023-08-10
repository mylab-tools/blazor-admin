using MyLab.BlazorAdmin.ComponentModel;

namespace MyLab.BlazorAdmin.Shared;

public partial class AdminLayout : IAlertPlace
{
    AlertDescription? _statusAlert;

    IDisposable IAlertPlace.PutStatusAlert(AlertDescription description)
    {
        _statusAlert = description;
        StateHasChanged();
        return new AlertRemover(this);
    }

    void RemoveAlert()
    {
        _statusAlert = null;
        StateHasChanged();
    }

    class AlertRemover : IDisposable
    {
        private readonly AdminLayout _layout;

        public AlertRemover(AdminLayout layout)
        {
            _layout = layout;
        }

        public void Dispose()
        {
            _layout.RemoveAlert();
        }
    }
}