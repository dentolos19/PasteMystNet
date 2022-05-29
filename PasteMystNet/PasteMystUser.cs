using System.Net;
using Newtonsoft.Json;
using PasteMystNet.Core;

namespace PasteMystNet;

public class PasteMystUser
{

    [JsonProperty("_id")] public string Id { get; private init; }
    [JsonProperty("username")] public string Username { get; private init; }
    [JsonProperty("avatarUrl")] public string AvatarUrl { get; private init; }
    [JsonProperty("defaultLang")] public string DefaultLanguage { get; private init; }
    [JsonProperty("publicProfile")] public bool IsPublicProfile { get; private init; }
    [JsonProperty("supporterLength")] public int SupporterLength { get; private init; }
    [JsonProperty("contributor")] public bool IsContributor { get; private init; }
    [JsonIgnore] public string ProfileUrl => PasteMystConstants.WebsiteUrl + "/users/" + Username;
    [JsonIgnore] public bool IsSupporter => SupporterLength > 0;

    [JsonProperty("stars")] public string[]? Stars { get; private init; }
    [JsonProperty("serviceIds")] public IDictionary<string, string>? ServiceIds { get; private init; }

    public static async Task<bool> UserExistsAsync(string username)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(string.Format(PasteMystConstants.UserExistsEndpoint, username));
        return response.StatusCode == HttpStatusCode.OK;
    }

    public static async Task<PasteMystUser> GetUserAsync(string username)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(string.Format(PasteMystConstants.GetUserEndpoint, username));
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PasteMystUser>(responseContent);
    }

    public static async Task<PasteMystUser> GetUserAsync(PasteMystToken token)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
        var response = await httpClient.GetAsync(PasteMystConstants.GetSelfEndpoint);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<PasteMystUser>(responseContent);
    }

    public static async Task<string[]> GetUserPastesAsync(PasteMystToken token)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
        var response = await httpClient.GetAsync(PasteMystConstants.GetSelfPastesEndpoint);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<string[]>(responseContent);
    }

}