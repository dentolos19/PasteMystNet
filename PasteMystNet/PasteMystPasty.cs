using Newtonsoft.Json;

namespace PasteMystNet
{
    /// <summary>
    /// This class is used to contain and store data.
    /// </summary>
    /// <seealso href="https://paste.myst.rs/api-docs/objects"/>
    public class PasteMystPasty
    {
        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "language")] public string Language { get; private set; }
        [JsonProperty(PropertyName = "title")] public string Title { get; private set; }
        [JsonProperty(PropertyName = "code")] public string Code { get; private set; }
    }
}