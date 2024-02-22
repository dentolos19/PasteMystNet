using System.Text.Json;

namespace PasteMystNet;

public partial class PasteMystClient
{
    public async Task<bool> UserExistsAsync(string username)
    {
        var response = await _httpClient.GetAsync($"user/{username}/exists");
        return response.IsSuccessStatusCode;
    }

    public async Task<PasteMystUser> GetUserAsync(string username)
    {
        var response = await _httpClient.GetAsync($"user/{username}");
        await PasteMystUtils.ValidateHttpResponse(response);
        return JsonSerializer.Deserialize<PasteMystUser>(await response.Content.ReadAsStringAsync())!;
    }

    public async Task<PasteMystUser> GetCurrentUserAsync()
    {
        var response = await _httpClient.GetAsync("user/self");
        await PasteMystUtils.ValidateHttpResponse(response);
        return JsonSerializer.Deserialize<PasteMystUser>(await response.Content.ReadAsStringAsync())!;
    }
}