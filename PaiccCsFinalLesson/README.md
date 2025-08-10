# GitHub Gist Creator

A simple command-line tool that creates private GitHub gists from a directory of files.

## Features

- Creates private gists on GitHub
- Supports multiple files from a directory
- Simple command-line interface
- Progress reporting and error handling
- Cross-platform (.NET 9)

## Prerequisites

- .NET 9.0 SDK
- GitHub Personal Access Token with gist creation permissions

## Setup

### 1. Clone and Build

```bash
dotnet build
```

### 2. Get a GitHub Token

1. Go to GitHub Settings → Developer settings → Personal access tokens → Tokens (classic)
2. Generate a new token with `gist` scope
3. Set it as an environment variable:

**Windows (PowerShell):**
```powershell
$env:GITHUB_TOKEN = "your_token_here"
```

**Windows (Command Prompt):**
```cmd
set GITHUB_TOKEN=your_token_here
```

**Linux/macOS:**
```bash
export GITHUB_TOKEN=your_token_here
```

## Usage

### Basic Usage

```bash
dotnet run -- --path <directory> --description <description>
```

### With Token Argument

```bash
dotnet run -- --path <directory> --description <description> --token <github_token>
```

### Examples

```bash
# Create a gist from the current modules directory
dotnet run -- --path ./modules --description "My C# modules"

# Create a gist from a specific folder
dotnet run -- --path "C:\MyProject\src" --description "Project source code"

# Use short argument names
dotnet run -- -p ./src -d "My awesome code"
```

## Arguments

| Argument | Short | Required | Description |
|----------|-------|----------|-------------|
| `--path` | `-p` | Yes | Directory containing files to include in the gist |
| `--description` | `-d` | Yes | Description for the gist |
| `--token` | `-t` | No* | GitHub personal access token |
| `--help` | `-h` | No | Show help message |

*Required if `GITHUB_TOKEN` environment variable is not set.

## Output

The tool will:
1. Read all files from the specified directory (including subdirectories)
2. Create a private gist on GitHub
3. Display the gist URL and ID

Example output:
```
GitHub Gist Creator
==================
Directory: ./modules
Description: My C# modules

Reading files from directory...
Found 4 files
Creating gist on GitHub...
✅ Gist created successfully!
🔗 Gist URL: https://gist.github.com/username/1234567890abcdef
📝 Gist ID: 1234567890abcdef
```

## Project Structure

```
PaiccCsFinalLesson/
├── Program.cs              # Main entry point and CLI argument parsing
├── modules/
│   ├── dataTypes.cs        # Gist, Files, and GistFile classes
│   ├── files.cs            # File reading functionality
│   ├── gist.cs             # GitHub gist creation
│   └── http.cs             # HTTP client for API calls
└── sample-files/           # Sample files for testing
```

## Error Handling

The application handles various error scenarios:
- Missing or invalid directory paths
- Missing GitHub token
- Network connectivity issues
- GitHub API errors
- File reading permissions

## Limitations

- Creates private gists only
- Reads all files in the directory (no file filtering)
- Requires internet connection
- Subject to GitHub API rate limits
