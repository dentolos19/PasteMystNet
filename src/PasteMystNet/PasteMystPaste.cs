using System.Text.Json.Serialization;

namespace PasteMystNet;

public class PasteMystPaste
{
    /// <summary>
    /// ID of the paste.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("_id")]
    public string Id { get; private set; }

    /// <summary>
    /// ID of the owner.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("ownerId")]
    public string OwnerId { get; private set; }

    /// <summary>
    /// Title of the paste.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("title")]
    public string Title { get; private set; }

    /// <summary>
    /// Unix time of when the paste was created.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("createdAt")]
    public long CreatedAt { get; private set; }

    /// <summary>
    /// How long the paste will expire from its creation time.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("expiresIn")]
    public string ExpiresIn { get; private set; }

    /// <summary>
    /// When the paste will be deleted. The value defaults to 0 when expiration is set to never.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("deletesAt")]
    public long DeletedAt { get; private set; }

    /// <summary>
    /// Number of stars the paste has.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("stars")]
    public int Stars { get; private set; }

    /// <summary>
    /// If it's private, it's only accessible by the owner.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("isPrivate")]
    public bool IsPrivate { get; private set; }

    /// <summary>
    /// If it's public, it will be displayed on the owner's profile.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("isPublic")]
    public bool IsPublic { get; private set; }

    /// <summary>
    /// List of tags.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("tags")]
    public IReadOnlyList<string> Tags { get; private set; }

    /// <summary>
    /// List of pasties.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("pasties")]
    public IReadOnlyList<PasteMystPasty> Pasties { get; private set; }

    /// <summary>
    /// List of edits.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("edits")]
    public IReadOnlyList<PasteMystEdit> Edits { get; private set; }

    public Uri Url => new($"https://paste.myst.rs/{Id}");
    public bool HasOwner => !string.IsNullOrEmpty(OwnerId);
    public DateTime CreatedAtTime => PasteMystUtils.ParseUnixTime(CreatedAt);
    public DateTime? DeletedAtTime => DeletedAt > 0 ? PasteMystUtils.ParseUnixTime(DeletedAt) : null;
}