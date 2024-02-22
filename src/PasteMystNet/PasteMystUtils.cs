using System.Text.Json;
using System.Text.Json.Nodes;

namespace PasteMystNet;

internal static class PasteMystUtils
{
    public static DateTime ParseUnixTime(long time)
    {
        return DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
    }

    public static async Task ValidateHttpResponse(HttpResponseMessage response)
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
            var exception = new PasteMystException(
                json?["statusMessage"]?.ToString() ?? "Unknown error.",
                requestException,
                response.RequestMessage.RequestUri,
                requestContent
            );
            throw exception;
        }
    }
}