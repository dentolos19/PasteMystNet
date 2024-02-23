using System.Text.Json.Serialization;

namespace PasteMystNet;

public class PasteMystCurrentUser : PasteMystUser
{
    /// <summary>
    /// List of paste IDs the user has starred.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("stars")]
    public IReadOnlyList<string> Stars { get; private set; }

    /// <summary>
    /// User IDs of the services that the user used to create an account.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("serviceIds")]
    public IReadOnlyDictionary<string, string> ServiceIds { get; private set; }
}