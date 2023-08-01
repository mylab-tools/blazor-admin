using System.Collections.Immutable;
using MyLab.BlazorAdmin.Tools;

namespace MyLab.BlazorAdmin.Shared.Dialogs
{
    /// <summary>
    /// Describes a dialog
    /// </summary>
    public class DialogDescription
    {
        /// <summary>
        /// Title
        /// </summary>
        public string? Title { get; init; }
        /// <summary>
        /// Specifies a modal-backdrop element
        /// </summary>
        public DialogBackdrop Backdrop { get; init; } = DialogBackdrop.False;
        /// <summary>
        /// Content component type
        /// </summary>
        public Type ContentType { get; }
        /// <summary>
        /// Footer component type
        /// </summary>
        public Type? FooterType { get; init; }
        /// <summary>
        /// Contains content initialization parameters 
        /// </summary>
        public InitParametersDictionary? ContentParameters { get; init; }
        /// <summary>
        /// Contains footer initialization parameters 
        /// </summary>
        public InitParametersDictionary? FooterParameters { get; init; }
        /// <summary>
        /// Contains dialog button descriptions
        /// </summary>
        public ImmutableArray<DialogButtonDescription> Buttons { get; init; }
        /// <summary>
        /// Initializes a new instance of <see cref="DialogDescription"/>
        /// </summary>
        public DialogDescription(Type contentType)
        {
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }
    }

    /// <summary>
    /// Describes the dialog button
    /// </summary>
    public class DialogButtonDescription
    {
        /// <summary>
        /// Title on the button
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// Gets callback action which invokes when button pressed
        /// </summary>
        public Action? Callback { get; init; }
        /// <summary>
        /// Gets or set primary button
        /// </summary>
        public bool Primary { get; set; }
        /// <summary>
        /// Initializes a new instance of <see cref="DialogButtonDescription"/>
        /// </summary>
        public DialogButtonDescription(string title)
        {
            Title = title;
        }
    }
}
