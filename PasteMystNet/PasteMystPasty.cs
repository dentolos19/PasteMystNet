using System.Text.Json.Serialization;

namespace PasteMystNet
{

    public class PasteMystPasty
    {

        [JsonPropertyName("_id")] public string Id { get; }
        [JsonPropertyName("language")] public PasteMystLanguage Language { get; } // TODO
        [JsonPropertyName("title")] public string Title { get; }
        [JsonPropertyName("code")] public string Code { get; }

    }

}