using System.Text.Json.Nodes;

namespace PasteMystNet;

public class PasteMystEditForm(PasteMystPaste paste)
{
    public string Id { get; } = paste.Id;
    public string Title { get; set; } = paste.Title;
    public bool IsPrivate { get; set; } = paste.IsPrivate;
    public bool IsPublic { get; set; } = paste.IsPublic;

    public IList<string> Tags { get; set; } =
        (IList<string>)paste.Tags;

    public IList<PasteMystPastyForm> Pasties { get; set; } =
        paste.Pasties.Select(pasty => new PasteMystPastyForm(pasty)).ToList();

    internal JsonObject ToJson()
    {
        var json = new JsonObject
        {
            ["title"] = Title,
            ["isPrivate"] = IsPrivate,
            ["isPublic"] = IsPublic,
            ["pasties"] = new JsonArray(Pasties.Select(pasty => (JsonNode)pasty.ToJson()).ToArray())
        };
        if (Tags is { Count: > 0 })
            json["tags"] = string.Join(',', Tags);
        return json;
    }
}