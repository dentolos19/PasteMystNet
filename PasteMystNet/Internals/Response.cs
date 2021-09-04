using Newtonsoft.Json;

namespace PasteMystNet.Internals
{

    internal class Response
    {

        [JsonProperty(PropertyName = "statusMessage")]
        public string Message { get; private set; }

    }

}