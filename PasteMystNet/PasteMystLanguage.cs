using Newtonsoft.Json;
using PasteMystNet.Internals;
using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;

namespace PasteMystNet
{

    /// <summary>
    /// This class is used to retrieve &amp; contain language information from server.
    /// </summary>
    /// <seealso href="https://paste.myst.rs/api-docs/data"/>
    public class PasteMystLanguage
    {

        [JsonProperty(PropertyName = "name")] public string Name { get; private set; }
        [JsonProperty(PropertyName = "mode")] public string Mode { get; private set; }
        [JsonProperty(PropertyName = "mimes")] public string[] Mimes { get; private set; }
        [JsonProperty(PropertyName = "ext")] public string[] Extensions { get; private set; }
        [JsonProperty(PropertyName = "color")] public string ColorHex { get; private set; }
        [JsonIgnore] public Color Color => Utilities.ParseColor(ColorHex);

        /// <summary>
        /// Identifies the language via name. Returns <c>null</c> when language can't be identified.
        /// </summary>
        public static async Task<PasteMystLanguage?> IdentifyByNameAsync(string name)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.IdentifyByNameEndpoint, Uri.EscapeDataString(name)));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystLanguage>(content);
        }

        /// <summary>
        /// Identifies the language via extension. Returns <c>null</c> when language can't be identified.
        /// </summary>
        public static async Task<PasteMystLanguage?> IdentifyByExtensionAsync(string extension)
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