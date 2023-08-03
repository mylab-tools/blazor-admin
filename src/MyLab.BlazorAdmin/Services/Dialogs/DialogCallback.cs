namespace MyLab.BlazorAdmin.Services.Dialogs;

/// <summary>
/// Describes dialog callback
/// </summary>
public delegate Task<bool> DialogCallback(DialogResult dialogResult, object? state);