using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PasteMystNet.Core;

namespace PasteMystNet
{

    public class PasteMystUser
    {

        [JsonPropertyName("_id")] public string Id { get; init; }
        [JsonPropertyName("username")] public string Username { get; init; }
        [JsonPropertyName("avatarUrl")] public string AvatarUrl { get; init; }
        [JsonPropertyName("defaultLang")] public string DefaultLanguage { get; init; }
        [JsonPropertyName("publicProfile")] public bool IsPublicProfile { get; init; }
        [JsonPropertyName("supporterLength")] public int SupporterLength { get; init; }
        [JsonPropertyName("contributor")] public bool IsContributor { get; init; }
        [JsonIgnore] public string ProfileUrl => Constants.WebsiteUrl + "/users/" + Username;
        [JsonIgnore] public bool IsSupporter => SupporterLength > 0;

        [JsonPropertyName("stars")] public string[]? Stars { get; private set; }
        [JsonPropertyName("serviceIds")] public IDictionary<string, string>? ServiceIds { get; private set; }

        public static async Task<bool> UserExistsAsync(string username)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.UserExistsEndpoint, username));
            return response.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<PasteMystUser?> GetUserAsync(string username)
        {
            var response = await Constants.HttpClient.GetAsync(string.Format(Constants.GetUserEndpoint, username));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PasteMystUser>(content);
        }

        public static async Task<PasteMystUser?> GetUserAsync(PasteMystToken token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await client.GetAsync(Constants.GetSelfEndpoint);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PasteMystUser>(content);
        }

        public static async Task<string[]?> GetUserPastesAsync(PasteMystToken token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await client.GetAsync(Constants.GetSelfPastesEndpoint);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<string[]>(content);
        }

    }

}