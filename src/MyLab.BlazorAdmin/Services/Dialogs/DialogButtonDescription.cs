namespace MyLab.BlazorAdmin.Services.Dialogs
{

    /// <summary>
    /// Describes the dialog button
    /// </summary>
    public class DialogButtonDescription
    {
        /// <summary>
        /// Title on the button
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Gets callback action which invokes when button pressed
        /// </summary>
        /// <remarks>Returns consent for dialog closing</remarks>
        public AsyncDialogCallback? Callback { get; set; }
        /// <summary>
        /// Gets or set primary button
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// Specifies dialog result when it button will be clicked
        /// </summary>
        /// <remarks><see cref="DialogResult.Undefined"/> by default</remarks>
        public DialogResult Result { get; set; } = DialogResult.Undefined;

        /// <summary>
        /// An object which will be passed into callback
        /// </summary>
        public object? State { get; set; }
    }
}
