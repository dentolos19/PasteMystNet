using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PasteMystNet.Core;

namespace PasteMystNet;

public class PasteMystPaste
{

    [JsonProperty("_id")] public string Id { get; private init; }
    [JsonProperty("ownerId")] public string OwnerId { get; private init; }
    [JsonProperty("title")] public string Title { get; private init; }
    [JsonProperty("stars")] public int Stars { get; private init; }
    [JsonProperty("isPrivate")] public bool IsPrivate { get; private init; }
    [JsonProperty("isPublic")] public bool IsPublic { get; private init; }
    [JsonProperty("encrypted")] public bool IsEncrypted { get; private init; }
    [JsonProperty("tags")] public string[]? Tags { get; private init; }
    [JsonProperty("pasties")] public PasteMystPasty[] Pasties { get; private init; }
    [JsonProperty("edits")] public PasteMystEdit[]? Edits { get; private init; }
    [JsonProperty("expiresIn")] public string ExpireDuration { get; private init; }
    [JsonProperty("createdAt")] public long CreationUnixTime { get; private init; }
    [JsonProperty("deletesAt")] public long DeletionUnixTime { get; private init; }
    [JsonIgnore] public string Url => PasteMystConstants.WebsiteUrl + $"/{Id}";
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
        if (token is not null)
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
        var response = await httpClient.GetAsync(string.Format(PasteMystConstants.GetPasteEndpoint, id));
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PasteMystPaste>(responseContent);
    }

    public static async Task DeletePasteAsync(string id, PasteMystToken token)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
        var response = await httpClient.DeleteAsync(string.Format(PasteMystConstants.DeletePasteEndpoint, id));
        response.EnsureSuccessStatusCode();
    }

    public static async Task<int> GetTotalActivePastesAsync()
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(PasteMystConstants.GetTotalActivePastesEndpoint);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var json = JsonConvert.DeserializeObject<JObject>(responseContent);
        return json["numPastes"].ToObject<int>();
    }

}