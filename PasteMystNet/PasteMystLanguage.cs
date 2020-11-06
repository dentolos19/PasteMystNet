﻿using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystLanguage
    {

        private const string IdentifyByNameEndpoint = "https://paste.myst.rs/api/v2/data/language?name={0}";
        private const string IdentifyByExtensionEndpoint = "https://paste.myst.rs/api/v2/data/languageExt?extension={0}";

        [JsonProperty(PropertyName = "color")] private readonly string _color;

        [JsonProperty(PropertyName = "name")] public string Name { get; private set; }
        [JsonProperty(PropertyName = "mode")] public string Mode { get; private set; }
        [JsonProperty(PropertyName = "mimes")] public string[] Mimes { get; private set; }
        [JsonProperty(PropertyName = "ext")] public string[] Extensions { get; private set; }
        [JsonIgnore] public Color Color => ParseColor(_color);

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
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(IdentifyByNameEndpoint, name));
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PasteMystLanguage>(content);
            }
        }

        public static async Task<PasteMystLanguage> IdentifyByExtensionAsync(string extension)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(IdentifyByExtensionEndpoint, extension));
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PasteMystLanguage>(content);
            }
        }

    }

}