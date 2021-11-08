using System.Text.Json.Serialization;

namespace PasteMystNet
{

    public class PasteMystPastyForm
    {

        [JsonPropertyName("_id")] internal string? Id { get; set; } // TODO: ignore null value

        [JsonPropertyName("title")] public string Title { get; set; } = string.Empty;
        [JsonPropertyName("language")] public string Language { get; set; } = "Autodetect";
        [JsonPropertyName("code")] public string Code { get; set; }

    }

}