using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystPasty
    {

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "language")] public string Language { get; private set; }
        [JsonProperty(PropertyName = "title")] public string Title { get; private set; }
        [JsonProperty(PropertyName = "code")] public string Code { get; private set; }

    }

}