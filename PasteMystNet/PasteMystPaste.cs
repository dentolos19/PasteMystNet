using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PasteMystNet;

public class PasteMystPaste
{

    [JsonProperty("_id")] public string Id { get; private set; }
    [JsonProperty("ownerId")] public string OwnerId { get; private set; }
    [JsonProperty("title")] public string Title { get; private set; }
    [JsonProperty("stars")] public int Stars { get; private set; }
    [JsonProperty("isPrivate")] public bool IsPrivate { get; private set; }
    [JsonProperty("isPublic")] public bool IsPublic { get; private set; }
    [JsonProperty("encrypted")] public bool IsEncrypted { get; private set; }
    [JsonProperty("tags")] public string[]? Tags { get; private set; }
    [JsonProperty("pasties")] public PasteMystPasty[] Pasties { get; private set; }
    [JsonProperty("edits")] public PasteMystEdit[]? Edits { get; private set; }
    [JsonProperty("expiresIn")] public string ExpireDuration { get; private set; }
    [JsonProperty("createdAt")] public long CreationUnixTime { get; private set; }
    [JsonProperty("deletesAt")] public long DeletionUnixTime { get; private set; }
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