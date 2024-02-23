using System.Text.Json.Nodes;

namespace PasteMystNet;

public class PasteMystPastyForm
{
    private string? _id = string.Empty;
    
    /// <summary>
    /// Language of the pasty.
    /// </summary>
    public string Language { get; set; } = "Autodetect";

    /// <summary>
    /// Title of the pasty.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Content of the pasty.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    public PasteMystPastyForm()
    {
        // empty
    }
    
    public PasteMystPastyForm(PasteMystPasty paste)
    {
        _id = paste.Id;
        Language = paste.Language;
        Title = paste.Title;
        Content = paste.Content;
    }
    
    internal JsonObject ToJson()
    {
        var json = new JsonObject
        {
            ["language"] = Language,
            ["title"] = Title,
            ["code"] = Content
        };
        if (_id is not null)
            json["_id"] = _id;
        return json;
    }
}