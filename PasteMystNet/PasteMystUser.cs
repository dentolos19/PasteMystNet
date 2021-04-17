using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasteMystNet.Internals;

namespace PasteMystNet
{

    /// <summary>
    /// This class is used to retrieve &amp; contain user information from server. <seealso href="https://paste.myst.rs/api-docs/user"/>
    /// </summary>
    public class PasteMystUser
    {

        [JsonProperty(PropertyName = "_id")] public string Id { get; private set; }
        [JsonProperty(PropertyName = "username")] public string Username { get; private set; }
        [JsonProperty(PropertyName = "avatarUrl")] public string AvatarUrl { get; private set; }
        [JsonProperty(PropertyName = "defaultLang")] public string DefaultLanguage { get; private set; }
        [JsonProperty(PropertyName = "publicProfile")] public bool HasPublicProfile { get; private set; }
        [JsonProperty(PropertyName = "supporterLength")] public uint SupporterLength { get; private set; }
        [JsonIgnore] public string ProfileUrl => PasteMystConstants.BaseEndpoint + "/users/" + Username;
        [JsonIgnore] public bool IsSupporter => SupporterLength != 0;

        /// <summary>
        /// Checks whether if user exists on server.
        /// </summary>
        public static async Task<bool> UserExistsAsync(string name)
        {
            var response = await PasteMystConstants.HttpClient.GetAsync(string.Format(PasteMystConstants.UserExistsEndpoint, name));
            return response.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Retrieves user's profile information from server. Returns <c>null</c> when user doesn't exists or didn't enable public profile.
        /// </summary>
        public static async Task<PasteMystUser?> GetUserAsync(string name)
        {
            var response = await PasteMystConstants.HttpClient.GetAsync(string.Format(PasteMystConstants.GetUserEndpoint, name));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PasteMystUser>(content);
        }

        public override string ToString()
        {
            try
            {
                return JsonConvert.SerializeObject(this, Formatting.Indented);
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case JsonException jsonError:
                        throw new Exception($"An error occurred during serialization: {jsonError.Message}");
                    default:
                        throw;
                }
            }
        }

    }

}