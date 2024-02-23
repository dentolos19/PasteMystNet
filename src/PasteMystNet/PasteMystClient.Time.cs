using System.Text.Json;
using System.Text.Json.Nodes;

namespace PasteMystNet;

public partial class PasteMystClient
{
    public async Task<long> ExpiresInToUnixTimeAsync(long createdAt, string expiresIn)
    {
        var response = await _httpClient.GetAsync(
            "time/expiresInToUnixTime?createdAt=" +
            createdAt +
            "&expiresIn=" +
            expiresIn
        );
        await PasteMystUtils.ValidateResponseAsync(response);
        var valueString =
            JsonSerializer.Deserialize<JsonObject>(
                await response.Content.ReadAsStringAsync()
            )?["result"];
        return int.TryParse(valueString?.ToString(), out var value) ? value : 0;
    }
}