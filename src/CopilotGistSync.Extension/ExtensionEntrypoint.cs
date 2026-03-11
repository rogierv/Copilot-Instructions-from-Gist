using CopilotGistSync.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Extensibility;

namespace CopilotGistSync.Extension;

/// <summary>
/// Extension entrypoint for the VisualStudio.Extensibility extension.
/// </summary>
[VisualStudioContribution]
internal class ExtensionEntrypoint : Microsoft.VisualStudio.Extensibility.Extension
{
    /// <inheritdoc/>
    public override ExtensionConfiguration ExtensionConfiguration => new()
    {
        Metadata = new(
                id: "CopilotGistSync.Extension.4cbb8886-6e67-467b-b7e3-9ae9de67496f",
                version: this.ExtensionAssemblyVersion,
                publisherName: "Rogier Verkaik",
                displayName: "GitHub Copilot Gist Sync",
                description: "Syncs GitHub Copilot instructions from a GitHub Gist to your solution"),
    };

    /// <inheritdoc />
    protected override void InitializeServices(IServiceCollection serviceCollection)
    {
        base.InitializeServices(serviceCollection);

        // Enable settings observers for monitoring setting changes
        serviceCollection.AddSettingsObservers();

        // Register Core services
        serviceCollection.AddSingleton<IGistClient, DefaultGistClient>();
        serviceCollection.AddSingleton<IFileSystem, PhysicalFileSystem>();
        serviceCollection.AddSingleton<ISyncService, SyncService>();
        serviceCollection.AddSingleton<AutoSyncHandler>();
    }
}
