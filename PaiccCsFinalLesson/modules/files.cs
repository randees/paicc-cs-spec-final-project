namespace GistCreator.Modules;

public static class FileManager
{
    public static Files PullFiles(string directoryPath)
    {
        try
        {
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
            }

            var files = new Files();
            var filePaths = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            if (filePaths.Length == 0)
            {
                throw new InvalidOperationException($"No files found in directory: {directoryPath}");
            }

            foreach (var filePath in filePaths)
            {
                try
                {
                    var content = File.ReadAllText(filePath);
                    var relativePath = Path.GetRelativePath(directoryPath, filePath);
                    
                    // Normalize path separators for consistency
                    relativePath = relativePath.Replace('\\', '/');
                    
                    files.AddFile(relativePath, content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Warning: Could not read file {filePath}: {ex.Message}");
                }
            }

            return files;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to pull files from directory: {ex.Message}", ex);
        }
    }
}
