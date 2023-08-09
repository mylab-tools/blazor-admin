using MyLab.BlazorAdmin.ComponentModel;

namespace MyLab.BlazorAdmin.Shared;

public partial class AdminLayout : IComponentRegistrar
{
    private object? _currentChild;

    /// <inheritdoc />
    public IDisposable RegisterChild(object child)
    {
        _currentChild = child;

        if (child is INavigationSource navSource)
        {
            _bottomNavPane = navSource.GetNavigation().ToArray();
            StateHasChanged();
        }

        return new ChildUnregistrar(child, UnregisterChild);
    }

    private void UnregisterChild(object child)
    {
        if (_currentChild != child)
            return;

        _bottomNavPane = null;
    }

    class ChildUnregistrar : IDisposable
    {
        private readonly object _child;
        private readonly Action<object> _unregisterAction;

        public ChildUnregistrar(object child, Action<object> unregisterAction)
        {
            _child = child;
            _unregisterAction = unregisterAction;
        }

        public void Dispose()
        {
            _unregisterAction(_child);
        }
    }
}