using Newtonsoft.Json;

namespace PasteMystNet.Internals
{
    
    internal class PasteMystFormJson
    {
        
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("expiresIn")]
        public string Expiration { get; set; }
        
        [JsonProperty("language")]
        public string Language { get; set; }
        
    }
    
}