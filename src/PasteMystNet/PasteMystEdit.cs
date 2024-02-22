using System.Text.Json.Serialization;

namespace PasteMystNet;

public class PasteMystEdit
{
    /// <summary>
    /// Unique ID of the edit.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("_id")]
    public string Id { get; private set; }
    
    /// <summary>
    /// ID of the edit. Multiple edits can share the same ID showing that multiple fields were changed at the same time.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("editId")]
    public string EditId { get; private set; }
    
    /// <summary>
    /// Type of edit.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("editType")]
    public PasteMystEditType EditType { get; private set; }
    
    /// <summary>
    /// Various metadata used internally. Biggest use case for this is storing exactly which pasty was edited.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("metadata")]
    public string[] Metadata { get; private set; }
    
    /// <summary>
    /// Actual paste edit. It stores old data before the edit modified the current paste.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("edit")]
    public string Edit { get; private set; }
    
    /// <summary>
    /// Unix time of when the edit was made.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("editedAt")]
    public long EditedAt { get; private set; }
}