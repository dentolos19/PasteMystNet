using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystPasteForm
    {

        private const string PostPasteEndpoint = "https://paste.myst.rs/api/v2/paste";

        [JsonProperty(PropertyName = "tags")] private string _tags;

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "expiresIn")] public PasteMystExpiration ExpireDuration { get; set; }
        [JsonProperty(PropertyName = "isPrivate")] public string IsPrivate { get; set; }
        [JsonProperty(PropertyName = "isPublic")] public string IsPublic { get; set; }
        [JsonProperty(PropertyName = "pasties")] public PasteMystPaste[] Pasties { get; set; }
        [JsonIgnore] public string[] Tags { get; set; }

        public async Task<string> PostPasteAsync(PasteMystAuth auth = null)
        {
            if (Tags == null || Tags.Length > 0 && auth == null)
                throw new ArgumentNullException(nameof(auth));
            _tags = string.Join(",", Tags);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (auth != null)
                    client.DefaultRequestHeaders.Authorization = auth.CreateAuthorization();
                var form = new StringContent(JsonConvert.SerializeObject(this));
                var response = await client.PostAsync(PostPasteEndpoint, form);
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                return await response.Content.ReadAsStringAsync();
            }
        }

    }

}