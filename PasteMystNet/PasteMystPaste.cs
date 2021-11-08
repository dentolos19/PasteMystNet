using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PasteMystNet.Core;

namespace PasteMystNet
{

    public class PasteMystPaste
    {

        [JsonPropertyName("_id")] public string Id { get; init; }
        [JsonPropertyName("ownerId")] public string OwnerId { get; init; }
        [JsonPropertyName("title")] public string Title { get; init; }
        [JsonPropertyName("stars")] public int Stars { get; init; }
        [JsonPropertyName("isPrivate")] public bool IsPrivate { get; init; }
        [JsonPropertyName("isPublic")] public bool IsPublic { get; init; }
        [JsonPropertyName("encrypted")] public bool IsEncrypted { get; init; }
        [JsonPropertyName("tags")] public string[]? Tags { get; init; }
        [JsonPropertyName("pasties")] public PasteMystPasty[] Pasties { get; init; }
        [JsonPropertyName("edits")] public PasteMystEdit[]? Edits { get; init; }
        [JsonPropertyName("expiresIn")] public string ExpireDuration { get; init; }
        [JsonPropertyName("createdAt")] public long CreationUnixTime { get; init; }
        [JsonPropertyName("deletesAt")] public long DeletionUnixTime { get; init; }
        [JsonIgnore] public string Url => Constants.WebsiteUrl + $"/{Id}";
        [JsonIgnore] public bool HasOwner => !string.IsNullOrEmpty(OwnerId);
        [JsonIgnore] public DateTime CreationTime => DateTimeOffset.FromUnixTimeSeconds(CreationUnixTime).DateTime;
        [JsonIgnore] public DateTime? DeletionTime => DeletionUnixTime <= 0 ? null : DateTimeOffset.FromUnixTimeSeconds(DeletionUnixTime).DateTime;

        public static async Task<PasteMystPaste?> GetPasteAsync(string id, PasteMystToken? token = null)
        {
            using var client = new HttpClient();
            if (token != null)
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await client.GetAsync(string.Format(Constants.GetPasteEndpoint, id));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PasteMystPaste>(content);
        }

        public static async Task<bool> DeletePasteAsync(string id, PasteMystToken token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await client.DeleteAsync(string.Format(Constants.DeletePasteEndpoint, id));
            return response.StatusCode == HttpStatusCode.OK;
        }

    }

}