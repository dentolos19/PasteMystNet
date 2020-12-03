using System.Drawing;
using System.Globalization;
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
        [JsonIgnore] public Color Color => ParseColor(ColorHex);

        private Color ParseColor(string hexadecimal)
        {
            hexadecimal = hexadecimal.TrimStart('#');
            Color result;
            if (hexadecimal.Length == 6)
            {
                result = Color.FromArgb(
                    255,
                    int.Parse(hexadecimal.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(4, 2), NumberStyles.HexNumber));
            }
            else
            {
                result = Color.FromArgb(
                    int.Parse(hexadecimal.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(4, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(6, 2), NumberStyles.HexNumber));
            }
            return result;
        }

        public static async Task<PasteMystLanguage> IdentifyByNameAsync(string name)
        {
            var response = await PasteMystConstants.HttpClient.GetAsync(string.Format(PasteMystConstants.IdentifyByNameEndpoint, name));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystLanguage>(content);
        }

        public static async Task<PasteMystLanguage> IdentifyByExtensionAsync(string extension)
        {
            var response = await PasteMystConstants.HttpClient.GetAsync(string.Format(PasteMystConstants.IdentifyByExtensionEndpoint, extension));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystLanguage>(content);
        }

    }

}