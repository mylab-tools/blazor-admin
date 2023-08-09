using System.Collections.Immutable;
using MyLab.BlazorAdmin.Shared.Dialogs;
using MyLab.BlazorAdmin.Tools;
using MyLab.BlazorAdmin.Tools.Rendering;

namespace MyLab.BlazorAdmin.Services.Dialogs;

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
    /// Content component 
    /// </summary>
    public RenderFragmentFactory Content { get; }
    /// <summary>
    /// Footer component type
    /// </summary>
    public RenderFragmentFactory? Footer { get; init; }
    /// <summary>
    /// Contains custom dialog button descriptions
    /// </summary>
    public ImmutableArray<DialogButtonDescription> CustomButtons { get; init; }
    /// <summary>
    /// Describes OK or Yes button
    /// </summary>
    public DialogButtonDescription? OkYesButton { get; init; }
    /// <summary>
    /// Describes Not button
    /// </summary>
    public DialogButtonDescription? NoButton { get; init; }
    /// <summary>
    /// Describes Cancel button
    /// </summary>
    public DialogButtonDescription? CancelButton { get; init; }
    /// <summary>
    /// Calls when dialog just closed
    /// </summary>
    public AsyncDialogCallback? DialogCallback { get; init; }
    /// <summary>
    /// Initializes a new instance of <see cref="DialogDescription"/>
    /// </summary>
    public DialogDescription(RenderFragmentFactory content)
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }
}