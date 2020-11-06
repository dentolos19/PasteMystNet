using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystPasty
    {

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "language")] public string Language { get; set; }
        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "code")] public string Code { get; set; }

    }

}