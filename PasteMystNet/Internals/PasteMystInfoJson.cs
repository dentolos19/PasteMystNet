using System.Text.Json;
using System.Text.Json.Serialization;

namespace PasteMystNet.Internals
{

    internal class PasteMystInfoJson
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("createdAt")]
        public uint Date { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("expiresIn")]
        public string Expiration { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static PasteMystInfoJson FromJson(string data)
        {
            return JsonSerializer.Deserialize<PasteMystInfoJson>(data);
        }

    }

}