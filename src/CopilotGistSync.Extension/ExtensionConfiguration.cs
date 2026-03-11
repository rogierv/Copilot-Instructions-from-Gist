using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Settings;

namespace CopilotGistSync.Extension;

#pragma warning disable VSEXTPREVIEW_SETTINGS // The settings API is currently in preview and marked as experimental

/// <summary>
/// Settings definitions for Copilot Gist Sync extension.
/// </summary>
internal static class SettingDefinitions
{
    [VisualStudioContribution]
    public static SettingCategory Category { get; } = new("copilotGistSync", "%CopilotGistSync.Extension.Settings.Category%")
    {
        Description = "%CopilotGistSync.Extension.Settings.Category.Description%",
        GenerateObserverClass = true,
    };

    [VisualStudioContribution]
    public static Setting.String GistUrlSetting { get; } = new(
        "gistUrl",
        "%CopilotGistSync.Extension.Settings.GistUrl%",
        Category,
        defaultValue: string.Empty)
    {
        Description = "%CopilotGistSync.Extension.Settings.GistUrl.Description%",
    };

    [VisualStudioContribution]
    public static Setting.Boolean EnableAutoSyncSetting { get; } = new(
        "enableAutoSync",
        "%CopilotGistSync.Extension.Settings.EnableAutoSync%",
        Category,
        defaultValue: false)
    {
        Description = "%CopilotGistSync.Extension.Settings.EnableAutoSync.Description%",
    };
}

#pragma warning restore VSEXTPREVIEW_SETTINGS
