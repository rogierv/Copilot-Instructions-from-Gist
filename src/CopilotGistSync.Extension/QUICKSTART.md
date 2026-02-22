# Quick Start - CopilotGistSync.Extension

## Setup (2 minutes)

### 1. Build and Run

```powershell
dotnet build src\CopilotGistSync.Extension\CopilotGistSync.Extension.csproj
```

Then press **F5** in Visual Studio to launch the Experimental Instance.

### 2. Configure Settings

In the Experimental Instance:

1. Go to **Tools → Options**
2. Navigate to **Copilot Gist Sync** in the left panel
3. Configure your settings:

   - **Gist URL**: Enter your GitHub Gist URL (e.g., `https://gist.github.com/username/abc123`)
   - **Enable Auto Sync**: Check this box to enable automatic sync on solution open

4. Click **OK** to save

### 3. Use the Extension

1. Open any solution
2. Go to **Tools → Sync Copilot Instructions from Gist**
3. The extension will download and save `.github/copilot-instructions.md`

## What You'll See

### First Run (File Created)
```
✓ Copilot instructions created from Gist.
```

### Subsequent Runs (File Updated)
```
✓ Copilot instructions updated from Gist.
```

### No Changes
```
ℹ Copilot instructions already up to date.
```

### Auto-Sync Enabled
When you open a solution (and run the command for the first time), it will automatically sync if `enableAutoSync` is `true`.

## Requirements

- Visual Studio 2022 17.14+ or Visual Studio 2026
- .NET 8 SDK
- A public GitHub Gist with a file named `copilot-instructions.md`

## Creating Your Gist

1. Go to https://gist.github.com/
2. Create a new Gist with:
   - **Filename**: `copilot-instructions.md`
   - **Content**: Your Copilot instructions
3. Make sure it's **public** (private Gists require authentication)
4. Copy the URL (e.g., `https://gist.github.com/username/abc123`)
5. Paste it into **Tools → Options → Copilot Gist Sync → Gist URL**

## Troubleshooting

### "No solution is currently open"
- Make sure you have a solution open before running the command

### "Please configure your Gist URL in Tools → Options"
- Go to **Tools → Options → Copilot Gist Sync**
- Enter your Gist URL
- Click OK

### "File 'copilot-instructions.md' not found in Gist"
- Verify your Gist contains a file named exactly `copilot-instructions.md`
- File name is case-sensitive

### HTTP Errors
- Verify the Gist URL is correct and publicly accessible
- Check your internet connection

### Auto-Sync Not Working
- Make sure **Enable Auto Sync** is checked in **Tools → Options**
- Auto-sync runs on first command execution per solution
- Check for any error messages in the Output window

## How It Works

### Manual Sync
1. You run **Tools → Sync Copilot Instructions from Gist**
2. Extension reads settings from **Tools → Options**
3. Downloads `copilot-instructions.md` from your Gist
4. Creates/updates `.github/copilot-instructions.md` in your solution

### Auto-Sync  
1. When you run the sync command for the first time after opening a solution
2. Extension checks if **Enable Auto Sync** is enabled
3. If yes, automatically syncs before showing results
4. Only syncs once per solution per VS session

## Comparison with VSIX

| Feature | VSIX | New Extension |
|---------|------|---------------|
| Manual Sync | ✅ | ✅ |
| Auto-Sync | ✅ Solution opened | ✅ First command run |
| Configuration | Tools → Options | Tools → Options |
| Stability | In-process | Out-of-process (more stable) |

## Settings API

The extension uses the official **VisualStudio.Extensibility Settings API**:

- ✅ Native Tools → Options integration
- ✅ Type-safe settings with validation
- ⚠️ Currently in preview (`VSEXTPREVIEW_SETTINGS`)
- ✅ Will be stable in future SDK releases

## Learn More

- See `README.md` for full documentation
- See `MIGRATION.md` for technical migration details
