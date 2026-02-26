using PlexTools.Core;

namespace PlexTools.API;

public class HttpApiRequester : IApiRequester
{
    private readonly HttpClient _httpClient;

    // Token is used to provide user privileges.
    public HttpApiRequester (string token)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-Plex-Token", token);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

  

    public async Task<string> SendRequestAsync(string url)
    {
        return await _httpClient.GetStringAsync(url);
    }
}