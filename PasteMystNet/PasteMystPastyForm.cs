using Newtonsoft.Json;

namespace PasteMystNet;

public class PasteMystPastyForm
{

    [JsonProperty("_id", NullValueHandling = NullValueHandling.Ignore)] internal string? Id { get; init; }

    [JsonProperty("title")] public string Title { get; set; } = string.Empty;
    [JsonProperty("language")] public string Language { get; set; } = "Autodetect";
    [JsonProperty("code")] public string Code { get; set; }

}