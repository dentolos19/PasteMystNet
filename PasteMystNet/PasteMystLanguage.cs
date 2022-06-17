using System.Drawing;
using Newtonsoft.Json;

namespace PasteMystNet;

public class PasteMystLanguage
{

    [JsonProperty("name")] public string Name { get; private set; }
    [JsonProperty("mode")] public string Mode { get; private set; }
    [JsonProperty("mimes")] public string[] Mimes { get; private set; }
    [JsonProperty("ext")] public string[] Extensions { get; private set; }
    [JsonProperty("color")] public string ColorHex { get; private set; }
    [JsonIgnore] public Color Color => (Color)new ColorConverter().ConvertFromString(ColorHex);

    public static async Task<PasteMystLanguage> GetLanguageByNameAsync(string name)
    {
        name = Uri.EscapeDataString(name); // make name percent-encoded
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(string.Format(PasteMystConstants.GetLanguageByNameEndpoint, Uri.EscapeDataString(name)));
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PasteMystLanguage>(responseContent);
    }

    public static async Task<PasteMystLanguage> GetLanguageByExtensionAsync(string extension)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(string.Format(PasteMystConstants.GetLanguageByExtensionEndpoint, Uri.EscapeDataString(extension)));
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PasteMystLanguage>(responseContent);
    }

}