using Newtonsoft.Json;

namespace PasteMystNet
{

    /// <summary>
    /// This class is used to contain and store data.
    /// </summary>
    /// <seealso href="https://paste.myst.rs/api-docs/objects"/>
    public class PasteMystPastyForm
    {

        [JsonProperty(PropertyName = "_id", NullValueHandling = NullValueHandling.Ignore)] internal string Id { get; set; }

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "language")] public string Language { get; set; }
        [JsonProperty(PropertyName = "code")] public string Code { get; set; }

    }

}