using GistCreator.Modules;
using System.Text.Json;

namespace GistCreator;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("GitHub Gist Creator");
            Console.WriteLine("==================");

            // Parse command line arguments
            var (directoryPath, description, githubToken) = ParseArguments(args);

            Console.WriteLine($"Directory: {directoryPath}");
            Console.WriteLine($"Description: {description}");
            Console.WriteLine();

            // Pull files from directory
            Console.WriteLine("Reading files from directory...");
            var files = FileManager.PullFiles(directoryPath);
            Console.WriteLine($"Found {files.GetFiles().Count} files");

            // Create gist object
            var gist = new Gist
            {
                Description = description,
                Public = false,
                Files = files
            };

            // Create gist on GitHub
            var result = await GistManager.CreateGist(gist, githubToken);
            
            Console.WriteLine("✅ Gist created successfully!");
            
            // Extract and display the gist URL
            if (result.TryGetValue("html_url", out var urlObj))
            {
                Console.WriteLine($"🔗 Gist URL: {urlObj}");
            }
            
            if (result.TryGetValue("id", out var idObj))
            {
                Console.WriteLine($"📝 Gist ID: {idObj}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    private static (string directoryPath, string description, string githubToken) ParseArguments(string[] args)
    {
        string? directoryPath = null;
        string? description = null;
        string? githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "--path":
                case "-p":
                    if (i + 1 < args.Length)
                    {
                        directoryPath = args[++i];
                    }
                    break;
                case "--description":
                case "-d":
                    if (i + 1 < args.Length)
                    {
                        description = args[++i];
                    }
                    break;
                case "--token":
                case "-t":
                    if (i + 1 < args.Length)
                    {
                        githubToken = args[++i];
                    }
                    break;
                case "--help":
                case "-h":
                    ShowUsage();
                    Environment.Exit(0);
                    break;
            }
        }

        if (string.IsNullOrEmpty(directoryPath))
        {
            Console.WriteLine("❌ Error: Directory path is required");
            ShowUsage();
            Environment.Exit(1);
        }

        if (string.IsNullOrEmpty(description))
        {
            Console.WriteLine("❌ Error: Description is required");
            ShowUsage();
            Environment.Exit(1);
        }

        if (string.IsNullOrEmpty(githubToken))
        {
            Console.WriteLine("❌ Error: GitHub token is required");
            Console.WriteLine("   Set the GITHUB_TOKEN environment variable or use --token argument");
            ShowUsage();
            Environment.Exit(1);
        }

        return (directoryPath, description, githubToken);
    }

    private static void ShowUsage()
    {
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run -- --path <directory> --description <description> [--token <github_token>]");
        Console.WriteLine();
        Console.WriteLine("Arguments:");
        Console.WriteLine("  --path, -p        Directory containing files to include in the gist");
        Console.WriteLine("  --description, -d Description for the gist");
        Console.WriteLine("  --token, -t       GitHub personal access token (or set GITHUB_TOKEN env var)");
        Console.WriteLine("  --help, -h        Show this help message");
        Console.WriteLine();
        Console.WriteLine("Example:");
        Console.WriteLine("  dotnet run -- --path ./src --description \"My awesome code\"");
    }
}
