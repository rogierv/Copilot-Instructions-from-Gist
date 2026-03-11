# Release Notes

## v1.0.0 - Initial Release (2025)

### ✨ Features
- **Public & Secret Gist Support** - Works with both public and secret gists for sharing instructions
- **One-way Sync** - Your Gist is never modified (Gist → Repository only)
- **Auto-sync on Solution Open** - Optional automatic synchronization when you open a solution
- **Manual Sync Command** - Sync on demand with a single click via Tools menu
- **Smart Updates** - Only updates `.github/copilot-instructions.md` when content actually changes
- **Status Bar Integration** - Visual progress indicator during sync operations
- **Non-intrusive** - No popup interruptions during auto-sync
- **Team Sharing** - Use secret gists to share instructions with your team privately

### 🎯 Supported Gist Types
- **Public Gists** - Anyone can see and sync from them (no authentication needed)
- **Secret Gists** - Only people with the link can access (great for team-wide standards)

### 🔧 Configuration
- Tools → Options → GitHub Copilot Gist Sync
- Enter your Gist URL (supports both standard and raw URL formats)
- Optionally enable auto-sync on solution open

### 📋 Requirements
- Visual Studio 2022 (version 17.0 or later)
- A GitHub Gist containing a file named `copilot-instructions.md`

### 🐛 Known Limitations
- This is the initial release with core syncing functionality
- UI is minimal and non-intrusive by design
- Only syncs the specific file named `copilot-instructions.md` from the Gist

### 📝 Notes
- The `.github` folder will be created if it doesn't exist
- Sync operations run asynchronously in the background
- Network errors during sync are reported in the status bar

---

## Future Roadmap

- Multi-file sync support (sync entire Gist folder)
- Conflict resolution options
- Sync history / rollback capability
- Performance metrics and sync logs
- Support for GitHub repositories and Azure Repos as sources
