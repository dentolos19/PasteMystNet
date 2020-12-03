using Newtonsoft.Json;

namespace PasteMystNet
{

    /// <summary>
    /// This class is used to contain and store data. <seealso href="https://paste.myst.rs/api-docs/objects"/>
    /// </summary>
    public class PasteMystPastyForm
    {

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)] internal string Id { get; set; }

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "language")] public string Language { get; set; }
        [JsonProperty(PropertyName = "code")] public string Code { get; set; }

    }

}