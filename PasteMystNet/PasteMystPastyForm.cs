using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystPastyForm
    {

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "language")] public string Language { get; set; }
        [JsonProperty(PropertyName = "code")] public string Code { get; set; }

    }

}