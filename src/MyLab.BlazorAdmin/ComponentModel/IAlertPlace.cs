using MyLab.BlazorAdmin.Tools.Rendering;

namespace MyLab.BlazorAdmin.ComponentModel;

/// <summary>
/// Represent an place to show an the alerts
/// </summary>
public interface IAlertPlace
{
    /// <summary>
    /// Adds new status alert
    /// </summary>
    IDisposable PutStatusAlert(AlertDescription description);
}

/// <summary>
/// Describes an alert
/// </summary>
public class AlertDescription
{
    /// <summary>
    /// Title text
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Content 
    /// </summary>
    public RenderFragmentFactory? Content { get; set; }
    /// <summary>
    /// Alert type
    /// </summary>
    public AlertType Type { get; set; }
}

/// <summary>
/// Defines an alert type
/// </summary>
public enum AlertType
{
    /// <summary>
    /// Undefined
    /// </summary>
    Undefined,
    /// <summary>
    /// Success
    /// </summary>
    Success,
    /// <summary>
    /// Warning
    /// </summary>
    Warning,
    /// <summary>
    /// Danger
    /// </summary>
    Danger
}