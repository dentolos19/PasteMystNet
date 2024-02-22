using System.Text;
using Newtonsoft.Json;

namespace PasteMystNet;

public class PasteMystEditForm
{
    [JsonIgnore] private readonly string _id;

    [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)] private string? _tags;

    [JsonProperty("title")] public string Title { get; set; }
    [JsonProperty("isPrivate", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPrivate { get; set; }
    [JsonProperty("isPublic", NullValueHandling = NullValueHandling.Ignore)] public bool? IsPublic { get; set; }
    [JsonProperty("pasties")] public IList<PasteMystPastyForm> Pasties { get; }
    [JsonIgnore] public IList<string> Tags { get; }

    internal PasteMystEditForm(PasteMystPaste paste)
    {
        _id = paste.Id;
        Title = paste.Title;
        Pasties = paste.Pasties is { Length: > 0 }
            ? paste.Pasties.Select(pasty => new PasteMystPastyForm // adds already defined pasty list from paste
            {
                Id = pasty.Id,
                Title = pasty.Title,
                Language = pasty.Language,
                Code = pasty.Code
            }).ToList()
            : new List<PasteMystPastyForm>(); // creates an empty list for pasties
        Tags = paste.Tags is { Length: > 0 }
            ? paste.Tags.ToList() // adds already defined tag list from paste
            : new List<string>(); // creates an empty list for tags
    }

    public async Task<PasteMystPaste> PatchPasteAsync(PasteMystToken token)
    {
        if (Tags.Count > 0)
            _tags = string.Join(",", Tags);
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
        var requestContent = JsonConvert.SerializeObject(this);
        var response = await httpClient.PatchAsync(string.Format(PasteMystConstants.PatchPasteEndpoint, _id), new StringContent(requestContent, Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PasteMystPaste>(responseContent);
    }
}