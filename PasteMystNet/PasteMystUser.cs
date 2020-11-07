using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PasteMystNet
{

    public class PasteMystUser
    {

        private const string UserExistsEndpoint = "https://paste.myst.rs/api/v2/user/{0}/exists";
        private const string GetUserEndpoint = "https://paste.myst.rs/api/v2/user/{0}";

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "username")] public string Username { get; private set; }
        [JsonProperty(PropertyName = "avatarUrl")] public string AvatarUrl { get; private set; }
        [JsonProperty(PropertyName = "defaultLang")] public string DefaultLanguage { get; private set; }
        [JsonProperty(PropertyName = "publicProfile")] public bool HasPublicProfile { get; private set; }
        [JsonProperty(PropertyName = "supporterLength")] public uint SupporterLength { get; private set; }
        [JsonIgnore] public bool IsSupporter => SupporterLength != 0;

        public static async Task<bool> UserExistsAsync(string name)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(string.Format(UserExistsEndpoint, name));
            return response.StatusCode == HttpStatusCode.OK;
        }

        public static async Task<PasteMystUser> GetUserAsync(string name)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(string.Format(GetUserEndpoint, name));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystUser>(content);
        }

    }

}