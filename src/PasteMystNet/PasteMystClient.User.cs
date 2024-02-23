using System.Text.Json;

namespace PasteMystNet;

public partial class PasteMystClient
{
    /// <summary>
    /// Checks if a user exists by their username.
    /// </summary>
    public async Task<bool> UserExistsAsync(string username)
    {
        var response = await _httpClient.GetAsync($"user/{username}/exists");
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Gets a user's information by their username.
    /// </summary>
    public async Task<PasteMystUser> GetUserAsync(string username)
    {
        var response = await _httpClient.GetAsync($"user/{username}");
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystUser>(await response.Content.ReadAsStringAsync())!;
    }

    /// <summary>
    /// Gets the current user's information. Requires authorization.
    /// </summary>
    public async Task<PasteMystCurrentUser> GetCurrentUserAsync()
    {
        var response = await _httpClient.GetAsync("user/self");
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystCurrentUser>(await response.Content.ReadAsStringAsync())!;
    }

    /// <summary>
    /// Gets the current user's paste IDs. Requires authorization.
    /// </summary>
    public async Task<IReadOnlyList<string>> GetCurrentUsersPastesAsync()
    {
        var response = await _httpClient.GetAsync("user/self/pastes");
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<string[]>(await response.Content.ReadAsStringAsync())!;
    }
}