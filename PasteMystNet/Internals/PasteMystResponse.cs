using Newtonsoft.Json;

namespace PasteMystNet.Internals
{
    internal class PasteMystResponse
    {
        [JsonProperty(PropertyName = "statusMessage")] public string Message { get; private set; }
    }
}