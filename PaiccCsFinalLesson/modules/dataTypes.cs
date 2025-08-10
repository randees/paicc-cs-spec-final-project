using System.Text.Json;

namespace GistCreator.Modules;

public class GistFile
{
    public string Content { get; set; } = string.Empty;
}

public class Files
{
    private Dictionary<string, GistFile> _files = new();

    public void AddFile(string fileName, string content)
    {
        _files[fileName] = new GistFile { Content = content };
    }

    public Dictionary<string, GistFile> GetFiles()
    {
        return _files;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(_files, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });
    }
}

public class Gist
{
    public string Description { get; set; } = string.Empty;
    public bool Public { get; set; } = false;
    public Files Files { get; set; } = new();

    public string ToJson()
    {
        var gistObject = new
        {
            description = Description,
            @public = Public,
            files = Files.GetFiles()
        };

        return JsonSerializer.Serialize(gistObject, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });
    }
}
