using Newtonsoft.Json;
using PasteMystNet.Core;
using System;
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
        [JsonProperty("pasties")] public IList<PasteMystPastyForm>? Pasties { get; set; } = new List<PasteMystPastyForm>();
        [JsonProperty("expiresIn")] public string ExpireDuration { get; set; } = PasteMystExpirations.Never;
        [JsonIgnore] public IList<string>? Tags { get; set; } = new List<string>();

        public async Task<PasteMystPaste> PostPasteAsync(PasteMystToken? token = null)
        {
            if ((IsPrivate.GetValueOrDefault() || IsPublic.GetValueOrDefault()) && token == null)
                throw new ArgumentNullException(nameof(token));
            if (Tags is { Count: > 0 } && token == null)
                throw new ArgumentNullException(nameof(token));
            if (Pasties is not { Count: > 0 })
                throw new Exception($"{nameof(Pasties)} must not be null or empty.");
            foreach (var paste in Pasties)
                if (string.IsNullOrEmpty(paste.Code))
                    throw new Exception($"{nameof(Pasties)}[{Pasties.IndexOf(paste)}] doesn't contain code content.");
            if (Tags != null)
                _tags = string.Join(",", Tags);
            using var httpClient = new HttpClient();
            if (token != null)
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var requestContent = JsonConvert.SerializeObject(this);
            var response = await httpClient.PostAsync(Constants.PostPasteEndpoint, new StringContent(requestContent, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystPaste>(responseContent);
        }

    }

}