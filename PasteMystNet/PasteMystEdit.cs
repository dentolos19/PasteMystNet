using System;
using System.Text.Json.Serialization;

namespace PasteMystNet
{

    public class PasteMystEdit
    {

        [JsonPropertyName("editedAt")] private readonly long _editedAt;
        [JsonPropertyName("editType")] private readonly string _editType;

        [JsonPropertyName("_id")] public string Id { get; init; }
        [JsonPropertyName("editId")] public string EditId { get; init; }
        [JsonPropertyName("metadata")] public string[] Metadata { get; init; }
        [JsonPropertyName("edit")] public string Edit { get; init; }
        [JsonIgnore] public PasteMystEditType EditType => (PasteMystEditType)Enum.Parse(typeof(PasteMystEditType), _editType);
        [JsonIgnore] public DateTime EditedTime => DateTimeOffset.FromUnixTimeSeconds(_editedAt).DateTime;

    }

}