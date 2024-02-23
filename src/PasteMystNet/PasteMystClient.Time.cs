using System.Text.Json;
using System.Text.Json.Nodes;

namespace PasteMystNet;

public partial class PasteMystClient
{
    /// <summary>
    /// Converts an expiration value to a specific time, in unix time, when a paste will expire.
    /// Returns <c>0</c> when unable to parse the value.
    /// </summary>
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

    /// <summary>
    /// Converts an expiration value to a specific time, in <see cref="DateTime"/>, when a paste will expire.
    /// Returns <c>0</c> when unable to parse the value.
    /// </summary>
    public async Task<DateTime> ExpiresInToDateTimeAsync(long createdAt, string expiresIn)
    {
        return PasteMystUtils.ParseUnixTime(await ExpiresInToUnixTimeAsync(createdAt, expiresIn));
    }
}