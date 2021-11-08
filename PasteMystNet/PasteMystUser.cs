using PasteMystNet.Core;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystUser
    {

        [JsonProperty("_id")] public string Id { get; init; }
        [JsonProperty("username")] public string Username { get; init; }
        [JsonProperty("avatarUrl")] public string AvatarUrl { get; init; }
        [JsonProperty("defaultLang")] public string DefaultLanguage { get; init; }
        [JsonProperty("publicProfile")] public bool IsPublicProfile { get; init; }
        [JsonProperty("supporterLength")] public int SupporterLength { get; init; }
        [JsonProperty("contributor")] public bool IsContributor { get; init; }
        [JsonIgnore] public string ProfileUrl => Constants.WebsiteUrl + "/users/" + Username;
        [JsonIgnore] public bool IsSupporter => SupporterLength > 0;

        [JsonProperty("stars")] public string[]? Stars { get; init; }
        [JsonProperty("serviceIds")] public IDictionary<string, string>? ServiceIds { get; init; }

        public static async Task<bool> UserExistsAsync(string username)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(string.Format(Constants.UserExistsEndpoint, username));
            return response.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<PasteMystUser> GetUserAsync(string username)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(string.Format(Constants.GetUserEndpoint, username));
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystUser>(responseContent);
        }

        public static async Task<PasteMystUser> GetUserAsync(PasteMystToken token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await httpClient.GetAsync(Constants.GetSelfEndpoint);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystUser>(responseContent);
        }

        public static async Task<string[]> GetUserPastesAsync(PasteMystToken token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token.ToString());
            var response = await httpClient.GetAsync(Constants.GetSelfPastesEndpoint);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string[]>(responseContent);
        }

    }

}