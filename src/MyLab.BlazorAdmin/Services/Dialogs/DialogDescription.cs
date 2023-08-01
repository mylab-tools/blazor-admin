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