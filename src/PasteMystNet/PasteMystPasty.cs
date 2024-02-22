using Newtonsoft.Json;

namespace PasteMystNet;

public class PasteMystPasty
{
    [JsonProperty("_id")] public string Id { get; private set; }
    [JsonProperty("language")] public string Language { get; private set; }
    [JsonProperty("title")] public string Title { get; private set; }
    [JsonProperty("code")] public string Code { get; private set; }
}