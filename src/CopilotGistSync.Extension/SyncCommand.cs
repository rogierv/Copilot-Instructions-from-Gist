using CopilotGistSync.Core;
using Microsoft;
using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;
using Microsoft.VisualStudio.Extensibility.Shell;
using System.Diagnostics;

namespace CopilotGistSync.Extension;

/// <summary>
/// Sync Copilot Instructions command handler.
/// </summary>
[VisualStudioContribution]
internal class SyncCommand : Command
{
    private readonly TraceSource logger;
    private readonly ISyncService syncService;
    private readonly AutoSyncHandler autoSyncHandler;
    private static bool hasCheckedAutoSync = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="SyncCommand"/> class.
    /// </summary>
    public SyncCommand(TraceSource traceSource, ISyncService syncService, AutoSyncHandler autoSyncHandler)
    {
        this.logger = Requires.NotNull(traceSource, nameof(traceSource));
        this.syncService = Requires.NotNull(syncService, nameof(syncService));
        this.autoSyncHandler = Requires.NotNull(autoSyncHandler, nameof(autoSyncHandler));
    }

    /// <inheritdoc />
    public override CommandConfiguration CommandConfiguration => new("%CopilotGistSync.Extension.SyncCommand.DisplayName%")
    {
        Icon = new(ImageMoniker.KnownValues.Sync, IconSettings.IconAndText),
    };

    /// <inheritdoc />
    public override async Task ExecuteCommandAsync(IClientContext context, CancellationToken cancellationToken)
    {
        try
        {
            // Get solution directory first
            var workspaces = this.Extensibility.Workspaces();
            var solutionQueryResult = await workspaces.QuerySolutionAsync(
                solution => solution,
                cancellationToken);

            string? solutionPath = null;
            string? solutionDir = null;

            foreach (var solutionSnapshot in solutionQueryResult)
            {
                var path = solutionSnapshot.Path;
                if (!string.IsNullOrEmpty(path))
                {
                    solutionPath = path;
                    solutionDir = Path.GetDirectoryName(path);
                    break;
                }
            }

            if (string.IsNullOrEmpty(solutionDir) || string.IsNullOrEmpty(solutionPath))
            {
                await ShowMessageAsync("No solution is currently open.", cancellationToken);
                return;
            }

            // Try auto-sync on first command execution (simulates solution opened event)
            if (!hasCheckedAutoSync)
            {
                hasCheckedAutoSync = true;
                await autoSyncHandler.TryAutoSyncAsync(solutionPath, cancellationToken);
            }

#pragma warning disable VSEXTPREVIEW_SETTINGS
            // Read Gist URL from settings
            var gistUrlResult = await this.Extensibility.Settings().ReadEffectiveValueAsync(
                SettingDefinitions.GistUrlSetting,
                cancellationToken);

            var gistUrl = gistUrlResult.ValueOrDefault(defaultValue: string.Empty);
#pragma warning restore VSEXTPREVIEW_SETTINGS

            if (string.IsNullOrWhiteSpace(gistUrl))
            {
                await ShowMessageAsync(
                    "Please configure your Gist URL in Tools → Options → Copilot Gist Sync",
                    cancellationToken);
                return;
            }

            logger.TraceInformation($"Manual sync from Gist: {gistUrl} to {solutionDir}");

            // Perform the sync
            var result = await this.syncService.SyncAsync(solutionDir, gistUrl);

            logger.TraceInformation($"Sync result: {result.Message}");

            // Show result to user
            await ShowMessageAsync(result.Message, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.TraceEvent(TraceEventType.Error, 0, $"Error syncing: {ex}");
            await ShowMessageAsync($"Error: {ex.Message}", cancellationToken);
        }
    }

    private async Task ShowMessageAsync(string message, CancellationToken cancellationToken)
    {
        await this.Extensibility.Shell().ShowPromptAsync(
            message,
            PromptOptions.OK,
            cancellationToken);
    }
}
