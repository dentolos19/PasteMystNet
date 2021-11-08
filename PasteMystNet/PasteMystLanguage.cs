using System;
using System.Drawing;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PasteMystNet.Core;

namespace PasteMystNet
{

    public class PasteMystLanguage
    {

        [JsonPropertyName("name")] public string Name { get; init; }
        [JsonPropertyName("mode")] public string Mode { get; init; }
        [JsonPropertyName("mimes")] public string[] Mimes { get; init; }
        [JsonPropertyName("ext")] public string[] Extensions { get; init; }
        [JsonPropertyName("color")] public string ColorHex { get; init; }
        [JsonIgnore] public Color Color => Utilities.ParseColorHex(ColorHex);

        public static async Task<PasteMystLanguage?> GetLanguageByNameAsync(string name)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.GetLanguageByNameEndpoint, Uri.EscapeDataString(name)));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PasteMystLanguage>(content);
        }

        public static async Task<PasteMystLanguage?> GetLanguageByExtensionAsync(string extension)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.GetLanguageByExtensionEndpoint, Uri.EscapeDataString(extension)));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PasteMystLanguage>(content);
        }

    }

}