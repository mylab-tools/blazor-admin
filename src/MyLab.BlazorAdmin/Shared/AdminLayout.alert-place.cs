using MyLab.BlazorAdmin.ComponentModel;

namespace MyLab.BlazorAdmin.Shared;

public partial class AdminLayout : IAlertPlace
{
    AlertDescription? _topAlert;

    IDisposable IAlertPlace.PutAlert(AlertDescription description)
    {
        _topAlert = description;
        StateHasChanged();
        return new AlertRemover(this);
    }

    void RemoveAlert()
    {
        _topAlert = null;
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