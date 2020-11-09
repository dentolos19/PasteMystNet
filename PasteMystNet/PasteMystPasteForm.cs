using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    public class PasteMystPasteForm
    {

        private const string PostPasteEndpoint = "https://paste.myst.rs/api/v2/paste";

        [JsonProperty(PropertyName = "tags")] private string _tags;
        [JsonProperty(PropertyName = "expiresIn")] private string _expiresIn;

        [JsonProperty(PropertyName = "title")] public string Title { get; set; }
        [JsonProperty(PropertyName = "isPrivate")] public bool IsPrivate { get; set; }
        [JsonProperty(PropertyName = "isPublic")] public bool IsPublic { get; set; }
        [JsonProperty(PropertyName = "pasties")] public PasteMystPastyForm[] Pasties { get; set; }
        [JsonIgnore] public string[] Tags { get; set; }
        [JsonIgnore] public PasteMystExpiration ExpireDuration { get; set; } = PasteMystExpiration.Never;

        public async Task<bool> PostPasteAsync(PasteMystAuth auth = null)
        {
            if ((Tags != null || Tags?.Length <= 0 || IsPrivate || IsPublic) && auth == null)
                throw new ArgumentNullException(nameof(auth));
            if (Pasties == null || Pasties.Length <= 0)
                throw new Exception(); // TODO
            if (Tags != null)
                _tags = string.Join(",", Tags);
            _expiresIn = ExpireDuration.GetStringRepresentation();
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (auth != null)
                client.DefaultRequestHeaders.Authorization = auth.CreateAuthorization();
            var form = new StringContent(JsonConvert.SerializeObject(this));
            var response = await client.PostAsync(PostPasteEndpoint, form);
            return response.StatusCode == HttpStatusCode.OK;
        }

    }

}