using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystPastyForm
    {

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)] internal string Id { get; set; }
        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "language")] public string Language { get; set; }
        [JsonProperty(PropertyName = "code")] public string Code { get; set; }

    }

}