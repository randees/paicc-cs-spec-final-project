namespace GistCreator.Modules;

public static class GistManager
{
    private const string GitHubApiUrl = "https://api.github.com/gists";

    public static async Task<Dictionary<string, object>> CreateGist(Gist gist, string githubToken)
    {
        try
        {
            var headers = new Dictionary<string, string>
            {
                { "Accept", "application/vnd.github+json" },
                { "Authorization", $"Bearer {githubToken}" },
                { "X-GitHub-Api-Version", "2022-11-28" },
                { "User-Agent", "GistCreator/1.0" }
            };

            var body = gist.ToJson();
            
            Console.WriteLine("Creating gist on GitHub...");
            var result = await Http.Post(GitHubApiUrl, headers, body);
            
            return result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to create gist: {ex.Message}", ex);
        }
    }
}
