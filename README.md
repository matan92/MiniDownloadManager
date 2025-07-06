# Mini Download Manager

## Overview
A simple Windows Forms application that downloads the highest-scored compatible file from a remote JSON list.

## Features
- Fetches file list from remote URL.
- Validates system compatibility (RAM, OS, Disk space).
- Displays the best file’s title and image.
- Downloads the file to temp folder and launches it.
- Prevents duplicate downloads on the same PC.
- Shows progress bar during operations.

## Dependencies
- [Newtonsoft.Json](https://www.newtonsoft.com/json) for JSON parsing.

## Usage
1. Run the executable (`MiniDownloadManager.exe`).
2. The app will auto-fetch and display the best compatible file.
3. Click the Download button to download and launch the file.

## Build
- Open the solution in Visual Studio 2022 (or later).
- Restore NuGet packages (Newtonsoft.Json).
- Build in Release mode.

## Notes
- Requires Windows OS.
- Tested with .NET 6+.
