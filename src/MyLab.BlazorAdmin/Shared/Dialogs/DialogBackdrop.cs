namespace MyLab.BlazorAdmin.Shared.Dialogs
{
    /// <summary>
    /// Specifies a modal-backdrop element
    /// </summary>
    public enum DialogBackdrop
    {
        /// <summary>
        /// Without backdrop element
        /// </summary>
        False,
        /// <summary>
        /// With backdrop element which close modal on click
        /// </summary>
        True,
        /// <summary>
        /// With backdrop element which does not close modal on click
        /// </summary>
        Static
    }
}
