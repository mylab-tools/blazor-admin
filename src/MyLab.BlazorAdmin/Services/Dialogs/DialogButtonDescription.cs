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
