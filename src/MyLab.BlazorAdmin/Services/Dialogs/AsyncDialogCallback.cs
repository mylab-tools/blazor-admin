namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Describes dialog callback
/// </summary>
public delegate Task<bool> AsyncDialogCallback(object dialog, DialogResult dialogResult, object? state);

/// <summary>
/// Describes dialog callback with custom dialog type
/// </summary>
public delegate Task<bool> AsyncDialogCallback<in TDialog>(TDialog dialog, DialogResult dialogResult, object? state);

/// <summary>
/// Describes dialog callback with custom dialog type
/// </summary>
public delegate bool DialogCallback<in TDialog>(TDialog dialog, DialogResult dialogResult, object? state);


/// <summary>
/// Describes dialog callback with custom dialog type and without dialog interruption
/// </summary>
public delegate Task OneWayAsyncDialogCallback<in TDialog>(TDialog dialog, DialogResult dialogResult, object? state);

/// <summary>
/// Describes dialog callback with custom dialog type and without dialog interruption
/// </summary>
public delegate void OneWayDialogCallback<in TDialog>(TDialog dialog, DialogResult dialogResult, object? state);