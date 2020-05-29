using Newtonsoft.Json;

namespace PasteMystNet
{

    internal class PasteMystInfoJson
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public uint Date { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("expiresIn")]
        public string Expiration { get; set; }
        
        [JsonProperty("language")]
        public string Language { get; set; }

    }

}