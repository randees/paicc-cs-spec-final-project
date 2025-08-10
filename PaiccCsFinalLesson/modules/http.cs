using System.Text;
using System.Text.Json;

namespace GistCreator.Modules;

public static class Http
{
    private static readonly HttpClient _httpClient = new();

    public static async Task<Dictionary<string, object>> Post(string url, Dictionary<string, string> headers, string body)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            // Add headers
            foreach (var header in headers)
            {
                if (header.Key.Equals("content-type", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // Already set in StringContent
                }
                request.Headers.Add(header.Key, header.Value);
            }

            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status {response.StatusCode}: {responseContent}");
            }

            var result = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
            return result ?? new Dictionary<string, object>();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"HTTP POST request failed: {ex.Message}", ex);
        }
    }
}
