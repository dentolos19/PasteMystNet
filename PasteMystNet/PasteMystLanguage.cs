using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystLanguage
    {

        [JsonProperty(PropertyName = "color")] private string _color;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; }

        [JsonProperty(PropertyName = "mode")]
        public string Mode { get; }

        [JsonProperty(PropertyName = "mimes")]
        public string[] Mimes { get; }

        [JsonProperty(PropertyName = "ext")]
        public string[] Extensions { get; }

        [JsonIgnore]
        public Color Color => ParseColor(_color);

        private Color ParseColor(string hexadecimal)
        {
            hexadecimal = hexadecimal.TrimStart('#');
            Color result;
            if (hexadecimal.Length == 6)
                result = Color.FromArgb(255,
                    int.Parse(hexadecimal.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(4, 2), NumberStyles.HexNumber));
            else
                result = Color.FromArgb(
                    int.Parse(hexadecimal.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(4, 2), NumberStyles.HexNumber),
                    int.Parse(hexadecimal.Substring(6, 2), NumberStyles.HexNumber));
            return result;
        }

        public static async Task<PasteMystLanguage> IdentifyByName(string name)
        {
            return null; // TODO
        }

        public static async Task<PasteMystLanguage> IdentifyByExtension(string name)
        {
            return null; // TODO
        }

    }

}