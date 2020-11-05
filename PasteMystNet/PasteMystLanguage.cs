using System.Text.Json.Serialization;

namespace PasteMystNet
{

    public class PasteMystLanguage
    {

        [JsonPropertyName("name")] public string Name { get; }
        [JsonPropertyName("mode")] public string Mode { get; }
        [JsonPropertyName("mimes")] public string[] Mimes { get; }
        [JsonPropertyName("ext")] public string[] Extensions { get; }
        [JsonPropertyName("color")] public string Color { get; } // TODO

        public static PasteMystLanguage ParseByName(string name)
        {
            return null; // TODO
        }

        public static PasteMystLanguage ParseByExtension(string extension)
        {
            return null; // TODO
        }

    }

}