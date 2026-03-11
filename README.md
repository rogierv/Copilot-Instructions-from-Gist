# Copilot Instructions from Gist

<div align="center">
  <img src="docs/screenshots/icon.png" alt="Extension Icon" width="128" height="128">
  <p><strong>Keep your GitHub Copilot instructions in sync across all your projects!</strong></p>
</div>

---

## 🎯 What is this?

**Copilot Instructions from Gist** is a Visual Studio extension that automatically syncs `copilot-instructions.md` from a GitHub Gist (public or secret) into your repository''s `.github` folder. 

No more copying and pasting instructions between projects. Create one Gist, configure once, and all your repositories stay in sync!

## 🤔 Why Was This Built?

**The Problem**: GitHub Copilot currently doesn't support global instruction files or shared instruction files across teams. If you work with multiple repositories, you need to manually copy and maintain `copilot-instructions.md` in each project's `.github` folder. This becomes cumbersome and error-prone, especially when:
- You have dozens of repositories
- Your team wants to share common coding standards
- You need to update instructions across all projects

**The Solution**: This extension enables you to:
- 📝 **Define once, use everywhere** - Maintain a single source of truth in a GitHub Gist
- 👥 **Share with your team** - Use secret gists to share team-wide instructions
- 🔄 **Stay synchronized** - Automatically sync instructions across all your repositories
- ⚡ **Save time** - No more manual copying and pasting

---

## ✨ Features

- ✅ **Public & Secret Gist Support** - Works with both public and secret gists
- ✅ **One-way Sync** - Your Gist is never modified (Gist → Repository only)
- ✅ **Auto-sync on Solution Open** - Optional automatic synchronization
- ✅ **Manual Sync Command** - Sync on demand with a single click
- ✅ **Smart Updates** - Only updates files when content actually changes
- ✅ **Status Bar Integration** - Visual progress indicator
- ✅ **Non-intrusive** - No popup interruptions during auto-sync
- ✅ **Team Sharing** - Use secret gists to share instructions with your team

---

## 📦 Installation

### From VSIX File

1. Download the latest `.vsix` file from the [Releases](https://github.com/rogierv/Copilot-Instructions-from-Gist/releases) page
2. Double-click the `.vsix` file to install
3. Restart Visual Studio
4. The extension is now installed!

### From Source

1. Clone this repository
2. Open the solution in Visual Studio 2022
3. Build the project in **Release** mode
4. Locate the generated `.vsix` file in `bin\Release\`
5. Double-click the `.vsix` file to install
6. Restart Visual Studio

---

## ⚙️ Configuration

### Step 1: Create a Gist

1. Go to [gist.github.com](https://gist.github.com)
2. Create a new Gist (can be **public** or **secret**)
   - **Public**: Anyone can see it (no authentication needed)
   - **Secret**: Only people with the link can access it (great for team sharing)
3. Name the file exactly: `copilot-instructions.md`
4. Add your Copilot instructions
5. Copy the Gist URL (e.g., `https://gist.github.com/username/1234567890abcdef`)

> **💡 Tip**: You can use either the Gist URL or the raw URL format: `https://gist.githubusercontent.com/username/1234567890abcdef/raw/copilot-instructions.md`

> **👥 Team Tip**: Use a secret gist to share team-specific coding standards without making them publicly visible!

### Step 2: Configure the Extension

1. Open Visual Studio 2022
2. Navigate to: **Tools** → **Options** → **GitHub Copilot Gist Sync**
3. Enter your **Gist URL** in the configuration field
4. (Optional) Enable **Auto Sync on Solution Open**
5. Click **OK** to save

![Configuration Screenshot](docs/screenshots/options-config.png)
*Screenshot: Configuration options in Visual Studio (Tools → Options → GitHub Copilot Gist Sync)*

---

## 🚀 Usage

### Manual Synchronization

Sync your Copilot instructions at any time:

1. In Visual Studio, go to: **Tools** → **Sync Copilot Instructions**
2. The extension will download and update `.github/copilot-instructions.md`
3. Check the Visual Studio status bar for progress

![Manual Sync Screenshot](docs/screenshots/manual-sync.png)
*Screenshot: Manual sync command in Tools menu*

### Automatic Synchronization

When **Auto Sync on Solution Open** is enabled:

- ✅ Sync runs automatically when you open a solution
- ✅ Progress is shown in the Visual Studio status bar
- ✅ No popup dialogs interrupt your workflow
- ✅ Sync is skipped if:
  - Auto Sync is disabled
  - No Gist URL is configured
  - The file is already up to date

---

## 📋 How It Works

1. You maintain a single `copilot-instructions.md` file in a GitHub Gist (public or secret)
2. The extension downloads the file from your Gist
3. It writes/updates `.github/copilot-instructions.md` in your repository
4. GitHub Copilot uses the file for context-aware suggestions

**Important**: 
- The extension **only reads** from your Gist (never writes)
- The `.github` folder will be created if it doesn''t exist
- Files are only updated when content actually changes (no unnecessary Git diffs)

### Flow Diagram

```
┌─────────────────────┐
│  GitHub Gist        │
│  (Public or Secret) │
└────────┬────────────┘
         │
         │ Download
         ▼
┌─────────────────┐
│   Extension     │
│   Downloads     │
└────────┬────────┘
         │
         │ Write/Update
         ▼
┌─────────────────────────────────┐
│  .github/copilot-instructions.md │
│  (Your Repository)               │
└──────────┬──────────────────────┘
           │
           │ Used by
           ▼
┌─────────────────┐
│ GitHub Copilot  │
│ (AI Assistant)  │
└─────────────────┘
```

---

## 🛠️ Requirements

- **Visual Studio 2022** (version 17.0 or later)
- **.NET Framework 4.7.2** (typically already installed with VS)
- A **GitHub Gist** (public or secret) containing a file named `copilot-instructions.md`

---

## 📝 Example Gist Structure

Your Gist (public or secret) should contain a file named exactly `copilot-instructions.md`:

```
📄 copilot-instructions.md
```

**Example content**:

```markdown
# Coding Standards

- Use C# 11 features where appropriate
- Follow Microsoft naming conventions
- Write XML documentation for public APIs
- Prefer async/await for I/O operations

# Project Conventions

- Use file-scoped namespaces
- Enable nullable reference types
- Target .NET 8.0 for new projects

# Architecture Patterns

- Follow SOLID principles
- Use dependency injection
- Implement repository pattern for data access
```

---

## 🎨 What Gets Created

After synchronization, your repository will have:

```
your-repository/
├── .github/
│   └── copilot-instructions.md  ← Created/updated by this extension
├── src/
├── ...
```

GitHub Copilot automatically reads `.github/copilot-instructions.md` to provide better, context-aware suggestions!

---

## 🔍 Troubleshooting

### "No Gist URL configured" message

**Solution**: Configure the Gist URL in **Tools → Options → GitHub Copilot Gist Sync**

### Sync doesn''t happen automatically

**Solution**: 
1. Check that **Auto Sync on Solution Open** is enabled
2. Verify your Gist URL is correct
3. Ensure the Gist URL is accessible (public or you have the secret link)

### File isn''t updating

**Solution**:
1. Verify your Gist contains a file named exactly `copilot-instructions.md`
2. Check that the Gist URL is accessible in a browser
3. Try running **Tools → Sync Copilot Instructions** manually

### "Failed to download Gist" error

**Solution**:
1. Verify the Gist URL is correct
2. Make sure you have access to the Gist (public gists work for everyone, secret gists require the correct URL)
3. Check your internet connection
4. Try accessing the Gist URL directly in a browser

---

## 🎯 Design Principles

This extension is built with the following principles in mind:

- **Source of truth lives in GitHub** - Your Gist is the single source of truth
- **Repository remains clean** - No unnecessary file modifications
- **No unnecessary Git diffs** - Files are only updated when content changes
- **No blocking UI** - All operations run asynchronously
- **Minimal user interruption** - Auto-sync runs silently in the background

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m ''Add some AmazingFeature''`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👤 Author

**Rogier Verkaik**

- GitHub: [@rogierv](https://github.com/rogierv)

---

## 💡 Use Cases

### Personal Developers
- Maintain consistent coding standards across all your personal projects
- Define your preferred patterns and practices once
- Automatically apply them to every repository you work on

### Teams & Organizations
- Share team-wide coding standards using a secret gist
- Ensure all team members have the same Copilot instructions
- Update instructions once, sync to all team repositories
- Onboard new developers with standardized AI assistance

### Multiple Projects
- No more copying `copilot-instructions.md` between repositories
- Update once in your Gist, sync everywhere
- Keep dozens or hundreds of repositories in sync effortlessly

**One source of truth. Many repositories. Zero hassle.**

---

## 🙋 FAQ

**Q: Can I use a secret/private Gist?**  
A: Yes! The extension supports both public and secret gists. Secret gists are perfect for sharing team-specific instructions without making them publicly visible.

**Q: Will this modify my Gist?**  
A: No! The synchronization is strictly one-way (Gist → Repository). Your Gist is never modified.

**Q: What if my repository already has a `copilot-instructions.md` file?**  
A: The extension will overwrite it with the content from your Gist if it differs.

**Q: Can I sync multiple Gists to different repositories?**  
A: You configure one Gist URL per Visual Studio instance. Each repository opened in that instance will sync from the same Gist.

**Q: Does this work with GitHub Copilot Chat?**  
A: Yes! GitHub Copilot (both inline and chat) uses `.github/copilot-instructions.md` for context.

---

<div align="center">
  <p>Made with ❤️ for the Visual Studio community</p>
  <p>⭐ If you find this useful, please star the repository!</p>
</div>
