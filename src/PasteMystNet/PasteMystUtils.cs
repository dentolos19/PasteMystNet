using System.Text.Json;
using System.Text.Json.Nodes;

namespace PasteMystNet;

internal static class PasteMystUtils
{
    public static DateTime ParseUnixTime(long time)
    {
        return DateTimeOffset.FromUnixTimeSeconds(time).DateTime.ToLocalTime();
    }

    public static async Task ValidateResponseAsync(HttpResponseMessage response)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException requestException)
        {
            var json = JsonSerializer.Deserialize<JsonObject>(await response.Content.ReadAsStringAsync());
            var requestContent = default(string);
            if (response.RequestMessage.Content is not null)
                requestContent = await response.RequestMessage.Content.ReadAsStringAsync();
            throw new PasteMystException(
                json?["statusMessage"]?.ToString() ?? "Unknown error. Please check the inner exception.",
                requestException,
                response.RequestMessage.RequestUri,
                requestContent
            );
        }
    }
}