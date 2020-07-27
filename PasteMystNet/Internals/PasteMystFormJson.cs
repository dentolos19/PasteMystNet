using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PasteMystNet.Internals
{

    internal class PasteMystFormJson
    {

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("expiresIn")]
        public string Expiration { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
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