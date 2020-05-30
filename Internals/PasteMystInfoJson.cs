using Newtonsoft.Json;

namespace PasteMystNet.Internals
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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static PasteMystInfoJson FromJson(string data)
        {
            return JsonConvert.DeserializeObject<PasteMystInfoJson>(data);
        }

    }

}