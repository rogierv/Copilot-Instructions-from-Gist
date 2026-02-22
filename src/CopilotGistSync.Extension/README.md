# Copilot Gist Sync - VisualStudio.Extensibility Extension

This is a migration of the CopilotGistSync.Vsix extension to the new **VisualStudio.Extensibility** model.

## Features

The extension synchronizes GitHub Copilot instructions from a GitHub Gist to your solution's `.github/copilot-instructions.md` file.

- ✅ Manual sync via Tools menu command
- ✅ Auto-sync on solution open (when enabled)
- ✅ Configurable via JSON settings file

## Project Structure

- **CopilotGistSync.Core**: Shared library containing the sync logic (used by both VSIX and Extension)
- **CopilotGistSync.Vsix**: Original VSIX extension (legacy model)
- **CopilotGistSync.Extension**: New VisualStudio.Extensibility extension

## What Was Migrated

### From VSIX to VisualStudio.Extensibility:

1. **Extension Entrypoint**
   - Migrated from `AsyncPackage` to `Extension` class
   - Set up dependency injection for Core services
   - Configured extension metadata

2. **Sync Command**
   - Migrated from `MenuCommand` to new `Command` base class
   - Uses Workspaces API to get solution path
   - Uses Shell API for user prompts
   - Placed in Tools menu

3. **Service Integration**
   - Reuses all Core library services (`ISyncService`, `IGistClient`, `IFileSystem`)
   - Configured via dependency injection in `ExtensionEntrypoint`

4. **Settings**
   - File-based configuration (JSON) instead of VS Options dialog
   - Supports both Gist URL and auto-sync enable/disable

5. **Auto-Sync on Solution Open**
   - Checks auto-sync setting when first command is executed per solution
   - Only syncs once per solution per session

## Configuration

Settings are managed through Visual Studio's **Tools → Options** dialog.

### Configuring the Extension

1. Open Visual Studio
2. Go to **Tools → Options**
3. Navigate to **Copilot Gist Sync** category
4. Configure the following settings:

#### Settings

- **Gist URL**: Your public GitHub Gist URL containing `copilot-instructions.md`
  - Example: `https://gist.github.com/username/abc123def456789`

- **Enable Auto Sync**: Check to automatically sync when opening a solution
  - When enabled, syncs automatically on first command execution per solution

### Finding Your Gist URL

1. Go to https://gist.github.com/
2. Create or open your Gist containing `copilot-instructions.md`
3. Copy the URL from the browser address bar
4. Example: `https://gist.github.com/username/abc123def456789`

## Building and Running

1. Build the solution in Visual Studio 2022 17.14+ or Visual Studio 2026
2. Press F5 to launch the Experimental Instance
3. Open a solution in the Experimental Instance
4. Go to **Tools → Sync Copilot Instructions from Gist**
5. The extension will sync the copilot-instructions.md file to your solution's `.github` folder

##How It Works

### Manual Sync

1. Run **Tools → Sync Copilot Instructions from Gist**
2. Extension loads settings from JSON file
3. Queries Visual Studio for current solution path
4. Downloads content from GitHub Gist
5. Creates or updates `.github/copilot-instructions.md`

### Auto-Sync

1. When you run the sync command for the first time in a solution, the extension checks if auto-sync is enabled
2. If enabled, it performs a sync automatically
3. Only syncs once per solution per VS session
4. Only shows a notification if the file was created or updated

## Comparison with VSIX Version

| Feature | VSIX | VisualStudio.Extensibility |
|---------|------|---------------------------|
| Manual Sync | ✅ | ✅ |
| Auto-Sync on Solution Open | ✅ | ✅ (via first command execution) |
| Settings UI | ✅ (Tools → Options) | ✅ (Tools → Options) |
| Status Bar Updates | ✅ | ❌ (API not available yet) |
| Process Model | In-process | Out-of-process |
| Threading | Manual | Automatic |

### Settings API

The extension uses the **preview Settings API** from VisualStudio.Extensibility. While in preview, this API provides:

- ✅ Full integration with Tools → Options dialog
- ✅ User-friendly settings UI
- ✅ Setting validation and type safety
- ✅ Reactive settings updates
- ⚠️ Marked as `VSEXTPREVIEW_SETTINGS` (will be finalized in future SDK releases)

## Advantages of VisualStudio.Extensibility

Despite using a JSON configuration file, this new model offers significant benefits:

- **Out-of-Process**: Runs in a separate process, making Visual Studio more stable
- **Cross-Platform Ready**: Designed to work with Visual Studio on all platforms  
- **Modern API**: Cleaner, more testable API surface
- **Better Performance**: Doesn't block the UI thread
- **Future-Proof**: Microsoft's recommended approach for new extensions

## API Documentation

- [VisualStudio.Extensibility Overview](https://learn.microsoft.com/en-us/visualstudio/extensibility/visualstudio.extensibility/)
- [Create Your First Extension](https://learn.microsoft.com/en-us/visualstudio/extensibility/visualstudio.extensibility/get-started/create-your-first-extension)
- [Commands Guide](https://learn.microsoft.com/en-us/visualstudio/extensibility/visualstudio.extensibility/command/command)
- [API Reference](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.extensibility)

## Troubleshooting

### "Please configure your Gist URL in Tools → Options"

1. Open **Tools → Options**
2. Navigate to **Copilot Gist Sync**
3. Enter your Gist URL in the **Gist URL** field
4. Click **OK**

### Auto-sync not working

1. Make sure **Enable Auto Sync** is checked in **Tools → Options → Copilot Gist Sync**
2. Auto-sync triggers on first command execution, not immediately on solution open
3. Check the Output window for trace logs

### File not found in Gist

Make sure your Gist contains a file named exactly `copilot-instructions.md` (case-sensitive).

## Next Steps

As the VisualStudio.Extensibility SDK evolves:

1. Migrate to proper Settings API when finalized
2. Add true solution-opened event handler when workspace events mature
3. Add status bar notifications when that API becomes available
4. Consider fully deprecating the VSIX version

## License

Same license as the parent repository.
