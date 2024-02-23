using System.Text.Json.Serialization;

namespace PasteMystNet;

public class PasteMystPasty
{
    /// <summary>
    /// ID of the pasty.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("_id")]
    public string Id { get; private set; }
    
    /// <summary>
    /// Language of the pasty.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("language")]
    public string Language { get; private set; }
    
    /// <summary>
    /// Title of the pasty.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("title")]
    public string Title { get; private set; }
    
    /// <summary>
    /// Content of the pasty.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("code")]
    public string Content { get; private set; }
}