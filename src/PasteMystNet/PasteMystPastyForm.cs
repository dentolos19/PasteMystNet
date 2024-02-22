using System.Text.Json.Nodes;

namespace PasteMystNet;

public class PasteMystPastyForm
{
    /// <summary>
    /// Language of the pasty.
    /// </summary>
    public string Language { get; set; } = "Autodetect";
    
    /// <summary>
    /// Title of the pasty.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Content of the pasty.
    /// </summary>
    public string Content { get; set; }
    
    internal JsonObject CreateJson()
    {
        var json = new JsonObject
        {
            ["language"] = Language,
            ["title"] = Title,
            ["code"] = Content
        };
        return json;
    }
}