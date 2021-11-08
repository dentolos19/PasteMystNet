using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystPasty
    {

        [JsonProperty("_id")] public string Id { get; init; }
        [JsonProperty("language")] public string Language { get; init; }
        [JsonProperty("title")] public string Title { get; init; }
        [JsonProperty("code")] public string Code { get; init; }

    }

}