using System.Collections.Immutable;
using MyLab.BlazorAdmin.Shared.Dialogs;
using MyLab.BlazorAdmin.Tools;

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
    public DialogDescription(Type contentType)
    {
        ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
    }
}