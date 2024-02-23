using System.Text.Json;
using System.Text.Json.Nodes;

namespace PasteMystNet;

public partial class PasteMystClient
{
    /// <summary>
    /// Gets the information for a specific language by name.
    /// </summary>
    public async Task<PasteMystLanguage> GetLanguageByNameAsync(string name)
    {
        var response = await _httpClient.GetAsync($"data/language?name={name}");
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystLanguage>(await response.Content.ReadAsStringAsync())!;
    }

    /// <summary>
    /// Gets the information for a specific language by extension.
    /// You don't need to include the leading dot in the extension.
    /// </summary>
    public async Task<PasteMystLanguage> GetLanguageByExtensionAsync(string extension)
    {
        var response = await _httpClient.GetAsync($"data/languageExt?extension={extension}");
        await PasteMystUtils.ValidateResponseAsync(response);
        return JsonSerializer.Deserialize<PasteMystLanguage>(await response.Content.ReadAsStringAsync())!;
    }

    /// <summary>
    /// Gets the number of currently active pastes.
    /// Returns <c>0</c> when unable to parse the value.
    /// </summary>
    public async Task<int> GetActivePastesAsync()
    {
        var response = await _httpClient.GetAsync("data/numPastes");
        await PasteMystUtils.ValidateResponseAsync(response);
        var valueString =
            JsonSerializer.Deserialize<JsonObject>(
                await response.Content.ReadAsStringAsync()
            )?["numPastes"];
        return int.TryParse(valueString?.ToString(), out var value) ? value : 0;
    }
}