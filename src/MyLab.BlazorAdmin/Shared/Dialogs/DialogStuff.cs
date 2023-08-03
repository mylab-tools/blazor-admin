using Microsoft.AspNetCore.Components;
using MyLab.BlazorAdmin.Services.Dialogs;

namespace MyLab.BlazorAdmin.Shared.Dialogs
{
    /// <summary>
    /// Describes dialog stuff
    /// </summary>
    public class DialogStuff
    {
        /// <summary>
        /// HTML element id
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Dialog description
        /// </summary>
        public DialogDescription Description { get; }
        /// <summary>
        /// Content template
        /// </summary>
        public RenderFragment Content { get; }
        /// <summary>
        /// footer template
        /// </summary>
        public RenderFragment? Footer { get; set; }

        /// <summary>
        /// Gets a dialog object
        /// </summary>
        public IDialog Dialog { get; }

        /// <summary>
        /// Initializes anew instance of <see cref="DialogStuff"/>
        /// </summary>
        public DialogStuff(string id, IDialog dialog, DialogDescription description, RenderFragment content)
        {
            Id = id;
            Description = description;
            Content = content;
            Dialog = dialog;
        }
    }
}
