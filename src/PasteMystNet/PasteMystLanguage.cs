using System.Text.Json.Serialization;

namespace PasteMystNet;

public class PasteMystLanguage
{
    /// <summary>
    /// Name of the language.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("name")]
    public string Name { get; private set; }

    /// <summary>
    /// Language mode to be used in the editor.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("mode")]
    public string Mode { get; private set; }

    /// <summary>
    /// List of mimes used by the language.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("mime")]
    public string[] Mimes { get; private set; }

    /// <summary>
    /// List of extensions used by the language.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("ext")]
    public IReadOnlyList<string> Extension { get; private set; }

    /// <summary>
    /// Color representing the language.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("color")]
    public string Color { get; private set; }
}