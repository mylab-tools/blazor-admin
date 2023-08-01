using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MyLab.BlazorAdmin.Shared.Dialogs
{
    /// <summary>
    /// Provide base functionality for dialogs
    /// </summary>
    public class DialogBase : ComponentBase, IAsyncDisposable
    {
        private BootstrapModalWrapper? _bootstrapModal;

        /// <summary>
        /// Dialog container
        /// </summary>
        protected DialogContainer? Container { get; set; }

        [Inject]
        private IJSRuntime? Js { get; set; }

        /// <summary>
        /// Opens dialog
        /// </summary>
        public async Task OpenAsync()
        {
            if(_bootstrapModal == null) return;
            await _bootstrapModal.ShowAsync();
        }

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Js != null && Container != null)
            {
                _bootstrapModal = new BootstrapModalWrapper(Js, Container.Id);
                //await _bootstrapModal.InitializeAsync(Container.Backdrop);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (_bootstrapModal != null)
                await _bootstrapModal.DisposeAsync();
        }
    }
}
