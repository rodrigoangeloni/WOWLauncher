# WoW Launcher - AI Coding Instructions

## Project Overview
This is a World of Warcraft custom launcher built with **WPF (.NET Framework 4.8)** and **C#**. It manages game client updates, server connections, and auto-login for private WoW servers. The solution contains two projects:
- **Launcher**: The main WPF application (player-facing)
- **Distribute Updates**: WinForms utility for server admins to generate patch metadata

## Architecture

### Core Components
1. **MainWindow.xaml.cs**: Main application logic (~860 lines)
   - Handles game launching, patch downloads, realmlist updates
   - Uses `Queue<PatchFileInfo>` for sequential patch downloading
   - Implements resumable downloads with MD5 hash verification
   - Download speed limiting with `StopWatch` timing control

2. **Configuration System**
   - **App.config**: Contains all server URLs (patch lists, news, updates, realmlist)
   - **Properties.Settings**: User preferences (login, password, game path, download speed)
   - Settings are stored in `%AppData%\Launcher\` directory

3. **HelpClasses/Utils.cs**: Critical game client detection and file handling
   - `ReamlistUtils`: Writes realmlist files for different WoW expansions
   - `Updater.GetPath()`: Routes .mpq patches to correct language folders
   - Supports 12 WoW language codes: enUS, esMX, ptBR, deDE, enGB, esES, frFR, itIT, ruRU, koKR, zhTW, zhCN

### WoW Expansion Support
The launcher detects and handles three WoW client versions:
- **Vanilla**: `realmlist.wtf` in game root
- **Lich King**: `Data\{lang}\realmlist.wtf` (localized)
- **Pandaria**: `WTF\config.wtf` with "set realmlist" entries

*Comment/uncomment the appropriate realmlist changer blocks in `MainWindow.xaml.cs` lines 269-290 based on target expansion.*

### Patch Download Flow
1. Download `patchlist.txt` from configured URL (format: `URL#Name#MD5#FileSize`)
2. Queue patches using `PatchFileInfo` class
3. Download to temp files named `{FileName}.{MD5Hash}.upd`
4. Verify MD5 hash before moving to game directory
5. Track completed patches in `UpdatedFiles.json` (supports resume)
6. Route .mpq files to `Data\` or `Data\{lang}\` based on naming convention

### Auto-Login Mechanism
Uses Windows API (`SendMessage` with WM_KEYDOWN/WM_CHAR) to inject keystrokes into the WoW client process:
- Sends username, TAB, password, ENTER to the running Wow.exe
- Requires delay timing (implemented via Mutex with handle waiting)

## Key Conventions

### File Paths
- Always use `Utilities.Updater.GetPath()` to determine patch destination
- Language-specific patches contain `-{lang}-` in filename (e.g., `patch-enUS-1.mpq`)
- Non-.mpq files go directly to game root

### Settings URLs (App.config)
All remote resources must be HTTPS URLs:
- `PatchDownloadURL`: Text file listing patches (format: URL#Name#MD5#Size)
- `PatchToDelete`: Text file listing patches to remove
- `RealmlistURL`: Text file containing realmlist content
- `LauncherVersionUrl`: Version check for launcher self-update
- `LauncherNewsFileUrl`: HTML/text for news display

### Assembly Branding
- Change `<AssemblyName>%25YourName%25</AssemblyName>` in `Launcher.csproj` (line 12)
- Update icon path in project properties (`img\101.ico`)

## Development Workflows

### Building
- Open `Launcher.sln` in Visual Studio 2015+ (targets .NET 4.8)
- Two configurations: Debug (uses VSHostingProcess disabled) and Release
- Output: `bin\Debug\` or `bin\Release\`

### Testing Patch System
1. Use **Distribute Updates** project to generate MD5 hashes
2. Create `patchlist.txt` with format: `http://url/patch.mpq#PatchName#MD5Hash#Bytes`
3. Update `PatchDownloadURL` in App.config
4. Launcher downloads to `%TEMP%` then verifies MD5 before moving

### Common Customization Points
- **Client detection**: Modify `MainWindow.xaml.cs` line 221+ (version detection logic)
- **Realmlist handling**: Enable/disable regions at lines 269-290
- **UI styling**: Edit `Style\LauncherStyle.xaml` for custom themes
- **Background images**: Replace `img\main.jpg`, `img\news_bg.jpg`

## Critical Implementation Details

### Singleton Instance Protection
`App.xaml.cs` uses a named Mutex (`F37E84CB-D76A-49B1-A1AC-80870903087B`) to prevent multiple launcher instances.

### JSON Serialization
Custom `JsonSerializer<T>` wrapper around `DataContractJsonSerializer` with `UseSimpleDictionaryFormat = true`. Used for persisting completed downloads state.

### Speed Limiting
Download throttling in `MainWindow.xaml.cs` lines 510-520:
- Calculates actual speed: `CurrentFileBytes2 * 1000 / StopWatch.Elapsed.TotalMilliseconds`
- Sleeps thread when exceeding `Properties.Settings.Default.DownloadSpeedLimit`

### Process Cleanup
Before launching game, checks if `Wow.exe` is already running and prevents launcher re-execution during gameplay.

## Common Pitfalls
- **MD5 hash must be lowercase** in patchlist.txt
- Realmlist files may have ReadOnly attribute - use `ClearReadOnlyAttributes()` before writing
- Language folders must exist before writing localized realmlists
- WebClient doesn't support resume natively - implemented via temp file tracking

## External Dependencies
- System.Runtime.Serialization (JSON serialization)
- System.Windows.Forms (FolderBrowserDialog)
- No NuGet packages - pure .NET Framework 4.8 BCL
