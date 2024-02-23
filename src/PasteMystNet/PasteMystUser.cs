using System.Text.Json.Serialization;

namespace PasteMystNet;

public class PasteMystUser
{
    /// <summary>
    /// ID of the user.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("_id")]
    public string Id { get; private set; }

    /// <summary>
    /// Username of the user.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("username")]
    public string Username { get; private set; }

    /// <summary>
    /// URL of their avatar.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("avatarUrl")]
    public Uri AvatarUrl { get; private set; }

    /// <summary>
    /// The default language of the user.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("defaultLang")]
    public string DefaultLanguage { get; private set; }

    /// <summary>
    /// Whether if they have a public profile.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("publicProfile")]
    public bool PublicProfile { get; private set; }

    /// <summary>
    /// How long has the user has been a supporter for. The value defaults to 0 if they are not a supporter.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("supporterLength")]
    public int SupporterLength { get; private set; }

    /// <summary>
    /// Whether if they are a contributor to <a href="https://paste.myst.rs">pastemyst</a>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("contributor")]
    public bool Contributor { get; private set; }

    public Uri ProfileUrl => new($"https://paste.myst.rs/users/{Username}");
    public bool IsSupporter => SupporterLength > 0;
}