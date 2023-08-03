using System.Net.Mime;

namespace MyLab.BlazorAdmin.Services.Dialogs
{
    /// <summary>
    /// Manages the dialogs
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Creates <see cref="IDialogBuilder{TContent}"/>
        /// </summary>
        /// <typeparam name="TContent">dialog content type</typeparam>
        /// <param name="title">title</param>
        IDialogBuilder<TContent> Create<TContent>(string title);
    }
}
