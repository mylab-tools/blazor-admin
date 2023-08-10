using System.Net.Mime;

namespace MyLab.BlazorAdmin.Services.Dialogs
{
    /// <summary>
    /// Manages the dialogs
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Creates <see cref="IDialogBuilder{TDialog}"/>
        /// </summary>
        /// <typeparam name="TDialog">dialog content type</typeparam>
        /// <param name="title">title</param>
        IDialogBuilder<TDialog> Create<TDialog>(string title);
    }
}
