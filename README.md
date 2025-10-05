```
# World of WarCraft Launcher

`Launcher` - A hobbyist toolkit for providing private WoW server players with quick access to automated patching, updates, and game launching features.

<p align="center">
  <img src="https://pp.vk.me/c631428/v631428303/500ff/wMh1l71dY5M.jpg" width="500" height="356"/>
</p>

---

### 🌍 Localized README
- 🇪🇸 [Español (Spanish)](README_ES.md)

---

## 📚 Documentation

For detailed setup instructions, see the original [README.docx](README.docx) or this comprehensive guide below.

---

## 🎮 Main Features

### Core Functionality

* **Automatic launcher self-update** - The launcher updates itself when new versions are available.
* **Cache cleanup** - Automatically removes the Cache folder from the WoW client.
* **Realmlist management** - Automatically updates the `realmlist.wtf` file with server connection details.
* **News display** - Shows server news directly in the launcher interface.
* **Patch downloading** (without requiring launcher updates):
  * MD5 hash verification (ensures patch integrity).
  * Download progress tracking (remaining time, speed, percentage, size).
  * Resumable downloads (continues from where it left off if interrupted).
* **Server patch removal**:
  * Allows players to leave the server cleanly (without affecting the launcher).
* **Unwanted patch cleanup**:
  * Useful for: rolling back failed updates, removing foreign patches except stock and server patches.
* **Launcher execution prevention**:
  * Prevents multiple launcher instances from running simultaneously.
  * Prevents launcher execution while the WoW client is running.

### Application Settings

The application provides user-configurable settings:

* **Automatic game login** using saved credentials:
  * Entered data remains anonymous by default (not transmitted).
* **Game client path specification**:
  * Explicit: Install launcher in WoW root directory.
  * Implicit: Select game folder manually via folder browser.
* **Customizable download speed limit**:
  * Network bandwidth limitation (KB/s, MB/s, GB/s).
* **Customizable download progress display**:
  * Total download progress across all patches.
  * Current file download progress.
  * Mixed progress display.

---

## 🛠️ Server Configuration

### Prerequisites

To use this launcher with your private WoW server, you need to host the following files on your web server:

1. **patchlist.txt** - List of patches to download
   - Format: `URL#FileName#MD5Hash#SizeInBytes`
   - Example: `https://yourserver.com/patch-1.mpq#patch-1.mpq#a1b2c3d4e5f6#1048576`

2. **patchtodel.txt** - List of patches to remove (optional)
   - Format: One filename per line
   - Example: `old-patch.mpq`

3. **realmlist.txt** - Realmlist content
   - Format: `set realmlist your.server.com`

4. **version.txt** - Current launcher version
   - Format: `1.0.0.0`

5. **news.txt** - News to display (HTML or plain text)

6. **updates.txt** - Launcher update history

### App.config Configuration

Edit the `Launcher/App.config` file and update the URLs:

```xml
<setting name="PatchDownloadURL" serializeAs="String">
    <value>https://yourserver.com/patchlist.txt</value>
</setting>
<setting name="PatchToDelete" serializeAs="String">
    <value>https://yourserver.com/patchtodel.txt</value>
</setting>
<setting name="RealmlistURL" serializeAs="String">
    <value>https://yourserver.com/realmlist.txt</value>
</setting>
<setting name="LauncherVersionUrl" serializeAs="String">
    <value>https://yourserver.com/version.txt</value>
</setting>
<setting name="LauncherNewsFileUrl" serializeAs="String">
    <value>https://yourserver.com/news.txt</value>
</setting>
<setting name="LauncherUpdates" serializeAs="String">
    <value>https://yourserver.com/updates.txt</value>
</setting>
```

### WoW Version Support

The launcher supports three main WoW versions:

#### **Vanilla (1.12.x)**
- Realmlist file: `realmlist.wtf` in game root
- Uncomment lines 269-275 in `MainWindow.xaml.cs`

#### **Wrath of the Lich King (3.3.5)**
- Realmlist file: `Data\{locale}\realmlist.wtf`
- Already enabled by default (lines 278-282)

#### **Mists of Pandaria (5.x)**
- Realmlist file: `WTF\config.wtf` with "set realmlist"
- Uncomment lines 285-291 in `MainWindow.xaml.cs`

**Important:** Comment/uncomment the appropriate blocks based on your server version.

---

## 📦 Building the Launcher

### Requirements
- Visual Studio 2015 or later (tested with VS 2022)
- .NET Framework 4.8
- Windows 7 or later

### Compilation Steps

1. Open `Launcher/Launcher.sln` in Visual Studio
2. Configure project properties:
   - **Solution Explorer** → Right-click **Launcher** → **Properties**
   - **Application** tab → **Assembly Information** button
   - Update: Title, Product, Company, Copyright
3. Update assembly name in `Launcher.csproj`:
   ```xml
   <AssemblyName>WoWLauncher</AssemblyName>
   ```
4. Build solution: **Build** → **Build Solution** (or press `Ctrl+Shift+B`)
5. Output location: `Launcher/bin/Release/WoWLauncher.exe`

### Customization

- **Icon**: Replace `img/101.ico` with your custom icon
- **Background**: Replace `img/main.jpg` with your server's background image
- **News background**: Replace `img/news_bg.jpg` for news panel styling
- **Styling**: Edit `Style/LauncherStyle.xaml` for custom themes

---

## 🔧 Distribute Updates Tool

The solution includes a **Distribute Updates** project (WinForms utility) for server administrators:

### Purpose
Generate MD5 hashes for patch files to create the `patchlist.txt` file.

### Usage
1. Open `Distribute Updates/Distribute Updates.sln` in Visual Studio
2. Run the application
3. Select patch files (`.mpq`, `.exe`, etc.)
4. Click **Generate** to calculate MD5 hashes
5. Copy output to create your `patchlist.txt` file

### Output Format
```
https://yourserver.com/patch-1.mpq#patch-1.mpq#a1b2c3d4e5f6789012345678901234#1048576
https://yourserver.com/patch-2.mpq#patch-2.mpq#b2c3d4e5f6789012345678901234567#2097152
```

---

## 🌐 Language Support

The launcher supports 12 WoW client languages for localized patch routing:
- `enUS` - English (United States)
- `esMX` - Spanish (Mexico)
- `ptBR` - Portuguese (Brazil)
- `deDE` - German
- `enGB` - English (Great Britain)
- `esES` - Spanish (Spain)
- `frFR` - French
- `itIT` - Italian
- `ruRU` - Russian
- `koKR` - Korean
- `zhTW` - Traditional Chinese
- `zhCN` - Simplified Chinese

Language-specific patches (containing `-{locale}-` in the filename) are automatically routed to `Data\{locale}\` folders.

---

## ⚠️ Important Warnings

1. **License Compliance**: This launcher is licensed under GPLv3. If you modify and distribute it, you **must** provide the source code to your users. Failure to comply may result in DMCA takedowns.

2. **Configuration URLs**: All URLs in `App.config` must use HTTPS for security.

3. **MD5 Hashes**: Must be lowercase in `patchlist.txt` for proper verification.

4. **File Permissions**: The launcher automatically clears ReadOnly attributes on realmlist files before writing.

5. **Process Management**: The launcher prevents execution if `Wow.exe` is already running to avoid conflicts.

---

## 📝 Patch File Naming Conventions

- **Language-specific patches**: `patch-{locale}-1.mpq` → Goes to `Data\{locale}\`
- **Generic patches**: `patch-1.mpq` → Goes to `Data\`
- **Executable updates**: `Wow.exe` → Goes to game root
- **Non-.mpq files**: Placed in game root directory

---

## 🐛 Troubleshooting

### Launcher won't start
- Check if another instance is already running
- Verify .NET Framework 4.8 is installed

### Patches not downloading
- Verify `patchlist.txt` URL is accessible (HTTPS)
- Check MD5 hashes are lowercase
- Ensure file sizes in bytes are correct

### Realmlist not updating
- Verify `realmlist.txt` URL is accessible
- Check correct WoW version block is uncommented in `MainWindow.xaml.cs`
- Ensure game path is correctly set in launcher settings

### Auto-login not working
- Verify username and password are saved in settings
- Check that delay timing is sufficient (default implementation uses Mutex waits)
- Ensure WoW client window is focused

---

## 📞 Feedback & Support

I welcome feedback on bugs, suggestions, and improvements. Feel free to:
- Create GitHub issues for bug reports
- Submit pull requests for enhancements
- Contact me directly using the details below

### Original Developer
* Гагауз Сергей (Gagauz Sergey)

### Contact Information
- *Telegram*: [@Jumper92](https://t.me/Jumper92)
- *E-Mail*: gaga-ya@hotmail.com

---

## 📄 License

WoW Launcher is distributed under the **GPLv3 license**. For more information, see the [LICENSE](LICENSE) file.

**You must provide source code to your users if you distribute modified versions.**
