using System.Text.Json.Nodes;

namespace PasteMystNet;

public class PasteMystPasteForm
{
    /// <summary>
    /// Title of the paste.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// When will the paste expire from now. Use values from <see cref="PasteMystExpirations"/> for this property.
    /// </summary>
    public string ExpiresIn { get; set; } = PasteMystExpirations.Never;

    /// <summary>
    /// If it is private, it's only accessible by the owner. Token is required.
    /// </summary>
    public bool? IsPrivate { get; set; }

    /// <summary>
    /// If it is public, it will be displayed on the owner's profile. Token is required.
    /// </summary>
    public bool? IsPublic { get; set; }

    /// <summary>
    /// List of tags. Token is required.
    /// </summary>
    public IList<string>? Tags { get; set; } = new List<string>();

    /// <summary>
    /// List of pasties.
    /// </summary>
    public IList<PasteMystPastyForm> Pasties { get; set; } = new List<PasteMystPastyForm>();

    internal JsonNode ToJson()
    {
        var json = new JsonObject
        {
            ["title"] = Title,
            ["expiresIn"] = ExpiresIn,
            ["pasties"] = new JsonArray(Pasties.Select(pasty => (JsonNode)pasty.ToJson()).ToArray())
        };
        if (IsPrivate is not null)
            json["isPrivate"] = IsPrivate;
        if (IsPublic is not null)
            json["isPublic"] = IsPublic;
        if (Tags is { Count: > 0 })
            json["tags"] = string.Join(',', Tags);
        return json;
    }
}