using System.Text.Json.Serialization;

namespace PasteMystNet
{

    public class PasteMystUser
    {

        [JsonPropertyName("_id")] public string Id { get; }
        [JsonPropertyName("username")] public string Username { get; }
        [JsonPropertyName("avatarUrl")] public string AvatarUrl { get; }
        [JsonPropertyName("defaultLang")] public string DefaultLanguage { get; }
        [JsonPropertyName("publicProfile")] public bool HasPublicProfile { get; }
        [JsonPropertyName("supporterLength")] public long SupporterLength { get; }
        [JsonIgnore] public bool IsSupporter => SupporterLength != 0;

        public static bool DoesUserExists(string name)
        {
            return false; // TODO
        }

        public static PasteMystUser GetUser(string name)
        {
            return null; // TODO
        }

    }

}