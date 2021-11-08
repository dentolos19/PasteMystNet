using PasteMystNet.Core;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystLanguage
    {

        [JsonProperty("name")] public string Name { get; init; }
        [JsonProperty("mode")] public string Mode { get; init; }
        [JsonProperty("mimes")] public string[] Mimes { get; init; }
        [JsonProperty("ext")] public string[] Extensions { get; init; }
        [JsonProperty("color")] public string ColorHex { get; init; }
        [JsonIgnore] public Color Color => ColorHex.ParseAsColorHex();

        public static async Task<PasteMystLanguage> GetLanguageByNameAsync(string name)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(string.Format(Constants.GetLanguageByNameEndpoint, Uri.EscapeDataString(name)));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystLanguage>(responseContent);
        }

        public static async Task<PasteMystLanguage> GetLanguageByExtensionAsync(string extension)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(string.Format(Constants.GetLanguageByExtensionEndpoint, Uri.EscapeDataString(extension)));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystLanguage>(responseContent);
        }

    }

}