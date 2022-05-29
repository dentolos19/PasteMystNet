using Newtonsoft.Json;

namespace PasteMystNet;

public class PasteMystPasty
{

    [JsonProperty("_id")] public string Id { get; private init; }
    [JsonProperty("language")] public string Language { get; private init; }
    [JsonProperty("title")] public string Title { get; private init; }
    [JsonProperty("code")] public string Code { get; private init; }

}