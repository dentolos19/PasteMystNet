using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PasteMystNet
{

    public class PasteMystUser
    {

        private const string UserExistsEndpoint = "https://paste.myst.rs/api/v2/user/{0}/exists";
        private const string GetUserEndpoint = "https://paste.myst.rs/api/v2/user/{0}";

        [JsonPropertyName("_id")] public string Id { get; set; }
        [JsonPropertyName("username")] public string Username { get; set; }
        [JsonPropertyName("avatarUrl")] public string AvatarUrl { get; set; }
        [JsonPropertyName("defaultLang")] public string DefaultLanguage { get; set; }
        [JsonPropertyName("publicProfile")] public bool HasPublicProfile { get; set; }
        [JsonPropertyName("supporterLength")] public uint SupporterLength { get; set; }

        public static async Task<bool> UserExistsAsync(string name)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(UserExistsEndpoint, name));
                return response.StatusCode == HttpStatusCode.OK;
            }
        }

        public static async Task<PasteMystUser> GetUserAsync(string name)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(string.Format(GetUserEndpoint, name));
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<PasteMystUser>(content);
            }
        }

    }

}