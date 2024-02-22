using System.Text.Json;
using System.Text.Json.Nodes;

namespace PasteMystNet;

internal static class PasteMystUtils
{
    public static DateTime ParseUnixTime(long unixTime)
    {
        return DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;
    }
    public static void ValidateHttpResponse(HttpResponseMessage httpResponse)
    {
        if (httpResponse.IsSuccessStatusCode) return;
        var responseContent = httpResponse.Content.ReadAsStringAsync().Result;
        var responseJson = JsonSerializer.Deserialize<JsonObject>(responseContent);
        throw new HttpRequestException(responseJson?["statusMessage"]?.ToString() ?? "Unknown error.");
    }
}