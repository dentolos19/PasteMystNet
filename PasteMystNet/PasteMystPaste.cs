using Newtonsoft.Json;
using PasteMystNet.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PasteMystNet
{

    public class PasteMystPaste
    {

        [JsonProperty("_id")] public string Id { get; init; }
        [JsonProperty("ownerId")] public string OwnerId { get; init; }
        [JsonProperty("title")] public string Title { get; init; }
        [JsonProperty("stars")] public int Stars { get; init; }
        [JsonProperty("isPrivate")] public bool IsPrivate { get; init; }
        [JsonProperty("isPublic")] public bool IsPublic { get; init; }
        [JsonProperty("encrypted")] public bool IsEncrypted { get; init; }
        [JsonProperty("tags")] public string[]? Tags { get; init; }
        [JsonProperty("pasties")] public PasteMystPasty[] Pasties { get; init; }
        [JsonProperty("edits")] public PasteMystEdit[]? Edits { get; init; }
        [JsonProperty("expiresIn")] public string ExpireDuration { get; init; }
        [JsonProperty("createdAt")] public long CreationUnixTime { get; init; }
        [JsonProperty("deletesAt")] public long DeletionUnixTime { get; init; }
        [JsonIgnore] public string Url => Constants.WebsiteUrl + $"/{Id}";
        [JsonIgnore] public bool HasOwner => !string.IsNullOrEmpty(OwnerId);
        [JsonIgnore] public DateTime CreationTime => DateTimeOffset.FromUnixTimeSeconds(CreationUnixTime).DateTime;
        [JsonIgnore] public DateTime? DeletionTime => DeletionUnixTime <= 0 ? null : DateTimeOffset.FromUnixTimeSeconds(DeletionUnixTime).DateTime;

        public PasteMystEditForm CreateEditForm()
        {
            return new PasteMystEditForm(this);
        }

        public static async Task<PasteMystPaste> GetPasteAsync(string id, PasteMystToken? token = null)
        {
            using var httpClient = new HttpClient();
            if (token != null)
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await httpClient.GetAsync(string.Format(Constants.GetPasteEndpoint, id));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystPaste>(responseContent);
        }

        public static async Task DeletePasteAsync(string id, PasteMystToken token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await httpClient.DeleteAsync(string.Format(Constants.DeletePasteEndpoint, id));
            response.EnsureSuccessStatusCode();
        }

    }

}