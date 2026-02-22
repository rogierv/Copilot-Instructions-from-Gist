using CopilotGistSync.Core;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Shell;
using System.Diagnostics;

namespace CopilotGistSync.Extension;

/// <summary>
/// Extension part that handles auto-sync on solution open.
/// </summary>
[VisualStudioContribution]
internal class AutoSyncHandler(TraceSource traceSource, ISyncService syncService) : ExtensionPart
{
    private readonly HashSet<string> syncedSolutions = new();

    /// <summary>
    /// Attempts to auto-sync if enabled and not already synced for this solution.
    /// This method should be called when a solution becomes available.
    /// </summary>
    public async Task TryAutoSyncAsync(string solutionPath, CancellationToken cancellationToken)
    {
        // Check if we've already synced this solution in this session
        if (syncedSolutions.Contains(solutionPath))
        {
            traceSource.TraceInformation($"Solution already auto-synced in this session: {solutionPath}");
            return;
        }

        try
        {
#pragma warning disable VSEXTPREVIEW_SETTINGS
            // Read settings
            var settingsService = Extensibility.Settings();
            var results = await settingsService.ReadEffectiveValuesAsync(
                [SettingDefinitions.EnableAutoSyncSetting, SettingDefinitions.GistUrlSetting],
                cancellationToken);

            var enableAutoSync = results.ValueOrDefault(SettingDefinitions.EnableAutoSyncSetting, defaultValue: false);
            var gistUrl = results.ValueOrDefault(SettingDefinitions.GistUrlSetting, defaultValue: string.Empty);
#pragma warning restore VSEXTPREVIEW_SETTINGS

            // Check if auto-sync is enabled
            if (!enableAutoSync)
            {
                traceSource.TraceInformation("Auto-sync is disabled");
                return;
            }

            // Check if Gist URL is configured
            if (string.IsNullOrWhiteSpace(gistUrl))
            {
                traceSource.TraceInformation("Gist URL not configured, skipping auto-sync");
                return;
            }

            var solutionDir = Path.GetDirectoryName(solutionPath);
            if (string.IsNullOrEmpty(solutionDir))
            {
                traceSource.TraceEvent(TraceEventType.Warning, 0, "Could not determine solution directory");
                return;
            }

            traceSource.TraceInformation($"Auto-syncing from Gist: {gistUrl} to {solutionDir}");

            // Perform the sync
            var result = await syncService.SyncAsync(solutionDir, gistUrl);

            traceSource.TraceInformation($"Auto-sync result: {result.Message}");

            // Mark this solution as synced
            syncedSolutions.Add(solutionPath);

            // Only show notification if there was an actual change
            if (result.ResultType != SyncResultType.Unchanged)
            {
                await this.Extensibility.Shell().ShowPromptAsync(
                    $"Copilot Gist Sync: {result.Message}",
                    Microsoft.VisualStudio.Extensibility.Shell.PromptOptions.OK,
                    cancellationToken);
            }
        }
        catch (Exception ex)
        {
            traceSource.TraceEvent(TraceEventType.Error, 0, $"Error during auto-sync: {ex}");
        }
    }
}
