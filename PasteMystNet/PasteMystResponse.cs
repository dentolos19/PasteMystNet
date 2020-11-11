using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystResponse
    {

        [JsonProperty(PropertyName = "statusMessage")] public string Message { get; private set; }

    }

}