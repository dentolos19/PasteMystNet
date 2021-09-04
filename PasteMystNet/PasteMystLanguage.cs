using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    public class PasteMystLanguage
    {

        [JsonProperty(PropertyName = "name")] public string Name { get; private set; }
        [JsonProperty(PropertyName = "mode")] public string Mode { get; private set; }
        [JsonProperty(PropertyName = "mimes")] public string[] Mimes { get; private set; }
        [JsonProperty(PropertyName = "ext")] public string[] Extensions { get; private set; }
        [JsonProperty(PropertyName = "color")] public string ColorHex { get; private set; }
        [JsonIgnore] public Color Color => Utilities.ParseColor(ColorHex);

        public static async Task<PasteMystLanguage?> GetLanguageByNameAsync(string name)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.IdentifyByNameEndpoint, Uri.EscapeDataString(name)));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystLanguage>(content);
        }

        public static async Task<PasteMystLanguage?> GetLanguageByExtensionAsync(string extension)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.IdentifyByExtensionEndpoint, Uri.EscapeDataString(extension)));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystLanguage>(content);
        }

        public override string ToString()
        {
            try
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case JsonException jsonError:
                        throw new Exception($"An error occurred during serialization: {jsonError.Message}");
                    default:
                        throw;
                }
            }
        }

    }

}