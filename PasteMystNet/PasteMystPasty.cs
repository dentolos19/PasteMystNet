using System.Text.Json.Serialization;

namespace PasteMystNet
{

    public class PasteMystPasty
    {

        [JsonPropertyName("_id")] public string Id { get; init; }
        [JsonPropertyName("language")] public string Language { get; init; }
        [JsonPropertyName("title")] public string Title { get; init; }
        [JsonPropertyName("code")] public string Code { get; init; }

    }

}