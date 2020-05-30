using System;
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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static PasteMystFormJson ToJson(PasteMystForm form)
        {
            return new PasteMystFormJson
            {
                Code = Uri.EscapeDataString(form.Code),
                Expiration = form.Expiration.GetStringRepresentation(),
                Language = form.Language.GetStringRepresentation()
            };
        }

    }
    
}