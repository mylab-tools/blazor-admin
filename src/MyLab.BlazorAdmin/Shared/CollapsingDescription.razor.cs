using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace MyLab.BlazorAdmin.Shared
{
    /// <summary>
    /// A description control which can collapse and save its state into local storage
    /// </summary>
    public partial class CollapsingDescription
    {
        bool _expanded;
        string? _storageStateId;

        [Inject] 
        private ILocalStorageService? LocalStorage { get; set; }

        /// <summary>
        /// Block content
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        /// <summary>
        /// Determines that the component is expanded if there is no saved state
        /// </summary>
        /// <remarks>
        /// true by default
        /// </remarks>
        [Parameter]
        public bool ExpandedByDefault { get; set; } = true;

        /// <summary>
        /// Determines a title when the component is in collapsed state
        /// </summary>
        [Parameter]
        public string CollapsedStateTitle { get; set; } = "More detailed...";

        /// <summary>
        /// Identifies a description for state saving into local storage
        /// </summary>
        [Parameter, EditorRequired]
        public string? Id { get; set; }

        /// <inheritdoc />
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (Id != null)
            {
                _storageStateId = $"collapsed-desc-state:{Id}";
                bool? expandedState = null;

                if (LocalStorage != null)
                {
                    expandedState = await LocalStorage.GetItemAsync<bool?>(_storageStateId);
                }

                _expanded = expandedState ?? ExpandedByDefault;
            }
            else
            {
                _expanded = ExpandedByDefault;
            }
        }

        private async Task CollapseRequestedAsync()
        {
            _expanded = false;

            await SaveStateAsync();
        }

        private async Task ExpandRequestedAsync()
        {
            _expanded = true;

            await SaveStateAsync();
        }

        private async Task SaveStateAsync()
        {
            if (_storageStateId != null && LocalStorage != null)
            {
                await LocalStorage.SetItemAsync(_storageStateId, _expanded);
            }
        }
    }
}
