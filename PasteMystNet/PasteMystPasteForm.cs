using Newtonsoft.Json;
using PasteMystNet.Core;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PasteMystNet
{

    public class PasteMystPasteForm
    {

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)] private string? _tags;

        [JsonProperty("title")] public string Title { get; set; } = string.Empty;
        [JsonProperty("isPrivate", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPrivate { get; set; }
        [JsonProperty("isPublic", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPublic { get; set; }
        [JsonProperty("pasties")] public IList<PasteMystPastyForm> Pasties { get; set; } = new List<PasteMystPastyForm>();
        [JsonProperty("expiresIn")] public string ExpireDuration { get; set; } = PasteMystExpirations.Never;
        [JsonIgnore] public IList<string>? Tags { get; set; } = new List<string>();

        public async Task<PasteMystPaste> PostPasteAsync(PasteMystToken? token = null)
        {
            if (Tags is not null)
                _tags = string.Join(",", Tags);
            using var httpClient = new HttpClient();
            if (token is not null)
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var requestContent = JsonConvert.SerializeObject(this);
            var response = await httpClient.PostAsync(Constants.PostPasteEndpoint, new StringContent(requestContent, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystPaste>(responseContent);
        }

    }

}