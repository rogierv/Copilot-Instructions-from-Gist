using Microsoft.VisualStudio.Extensibility;
using Microsoft.VisualStudio.Extensibility.Commands;

namespace CopilotGistSync.Extension;

/// <summary>
/// Defines the GitHub Copilot Instructions menu in the Extensions menu.
/// </summary>
public static class GitHubCopilotInstructionsMenu
{
    [VisualStudioContribution]
    public static MenuConfiguration Menu => new("%CopilotGistSync.Extension.Menu.DisplayName%")
    {
        Placements = [CommandPlacement.KnownPlacements.ExtensionsMenu.WithPriority(0x0001)],
        Children =
        [
            MenuChild.Command<SyncCommand>(),
        ],
    };
}
