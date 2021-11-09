using Newtonsoft.Json;
using PasteMystNet.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PasteMystNet
{

    public class PasteMystEditForm
    {

        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)] private string? _tags;
        [JsonIgnore] private readonly string _id;

        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("isPrivate", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPrivate { get; set; }
        [JsonProperty("isPublic", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPublic { get; set; }
        [JsonProperty("pasties")] public IList<PasteMystPastyForm> Pasties { get; }
        [JsonIgnore] public IList<string> Tags { get; }

        internal PasteMystEditForm(PasteMystPaste paste)
        {
            _id = paste.Id;
            Title = paste.Title;
            Pasties = paste.Pasties.Select(pasty => new PasteMystPastyForm
            {
                Id = pasty.Id,
                Title = pasty.Title,
                Language = pasty.Language,
                Code = pasty.Code
            }).ToList();
            Tags = paste.Tags is { Length: > 0 } ? paste.Tags.ToList() : new List<string>();
        }

        public async Task<PasteMystPaste> PatchPasteAsync(PasteMystToken token)
        {
            if (Pasties is not { Count: > 0 })
                throw new Exception($"{nameof(Pasties)} must not be null or empty.");
            foreach (var paste in Pasties)
                if (string.IsNullOrEmpty(paste.Code))
                    throw new Exception($"{nameof(Pasties)}[{Pasties.IndexOf(paste)}] doesn't contain code content.");
            if (Tags.Count > 0)
                _tags = string.Join(",", Tags);
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var requestContent = JsonConvert.SerializeObject(this);
            var response = await httpClient.PatchAsync(string.Format(Constants.PatchPasteEndpoint, _id), new StringContent(requestContent, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystPaste>(responseContent);
        }

    }

}